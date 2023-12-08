using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : IStoreListener
{


    public class ProductInfo
    {
        public string ID { private set; get; }
        public ProductType Type { private set; get; }

        public ProductInfo(string id, ProductType type)
        {
            ID = id;
            Type = type;
        }
    }

    public enum BuyFailureError
    {
        None,
        NotInitialization,
        UnknownError,
        UnknownItem,
        NetworkError,
    }

    /// <summary>
    /// �ۋ��������̃f���Q�[�g
    /// </summary>
    public delegate void SuccessEvent(UnityEngine.Purchasing.Product product);

    /// <summary>
    /// �ۋ������ʒm�C�x���g
    /// </summary>
    private SuccessEvent OnPurchaseSuccessResult;

    /// <summary>
    /// �ۋ����s���̃f���Q�[�g
    /// </summary>
    public delegate void FailureEvent(UnityEngine.Purchasing.Product product, PurchaseFailureReason error);

    /// <summary>
    /// �ۋ����s�ʒm�C�x���g
    /// </summary>
    private FailureEvent OnPurchaseFailureResult;

    public static bool IsInitialized => storeController != null && storeExtensionProvider != null;

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    /// <summary>
    /// �C���X�^���X
    /// </summary>
    private static PurchaseManager instance;

    /// <summary>
    /// �V���O���g��
    /// </summary>
    public static PurchaseManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PurchaseManager();
            }
            return instance;
        }
    }


    public void Initialize()
    {
        if(!IsInitialized)
        {
            // �����ōw���\�ȃA�C�e����ݒ肷��
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("com.example.productid", ProductType.Consumable);
            UnityPurchasing.Initialize(this, builder);
        }

    }

    public BuyFailureError Buy(ProductInfo productInfo, SuccessEvent OnPurchasSuccess, FailureEvent OnPurchasFailure)
    {

        try
        {
            if (IsInitialized)
            {
                UnityEngine.Purchasing.Product product = storeController.products.WithID(productInfo.ID);
                // �����ŁA�w���\���ǂ����̔�����s��
                if (product == null && !product.availableToPurchase)
                {
                    Debug.LogError($"BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    return BuyFailureError.UnknownItem;

                }

                if(!hasNetworkConnection())
                {
                    // �l�b�g���[�N�G���[�̃��O���o��
                    Debug.LogError("disconnected from network");
                    return BuyFailureError.NetworkError;
                }

                // �w���������J�n����
                Debug.Log($"Purchasing product asychronously: {product.definition.id}");
                OnPurchaseSuccessResult = OnPurchasSuccess;
                OnPurchaseFailureResult = OnPurchasFailure;
                storeController.InitiatePurchase(product);
                return BuyFailureError.None;

            }
            else
            {
                Debug.LogError("PurchaseManager is not initialized.");
                return BuyFailureError.NetworkError;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"PurchaseManager Buy Error:{e.Message}");
            return BuyFailureError.UnknownError;
        }

    }

    private void InvokeFailureEvent(PurchaseFailureReason buyFailureError, UnityEngine.Purchasing.Product product = null)
    {
        OnPurchaseFailureResult?.Invoke(product, buyFailureError);
        OnPurchaseSuccessResult = null;
        OnPurchaseFailureResult = null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("PurchaseManager OnInitialized");
        storeController = controller;
        storeExtensionProvider = extensions;
        // �T�[�o�[���烌�V�[�g�̏�Ԃ��擾���A�w���ۗ����ł���ۂ̏���������
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"PurchaseManager OnInitializeFailed Error:{error.ToString()}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"PurchaseManager OnInitializeFailed Error:{error.ToString()} Message:{message}");
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        InvokeFailureEvent(failureReason);
        Debug.LogError($"PurchaseManager OnInitializeFailed Error:{failureReason.ToString()} product:{product.ToString()}");
    }

    // �w���������̏���
    // PurchaseProcessingResult.Complete���Ă΂��O�ɃA�v�����I�������ꍇ�A����N�����Ɏ����ŃR�[�������
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        try
        {
            // �����ŁA�w���������̏������s��
            if (String.Equals(purchaseEvent.purchasedProduct.definition.id, "com.example.productid", StringComparison.Ordinal))
            {
                Debug.Log("You've just purchased the product!");
                // �����ŁA�Ⴆ�΃Q�[�����ʉ݂�ǉ�����ȂǁA��̓I�ȍw���������̏������s��
                // �T�[�o�[�ŒʐM���s���ۂ�PurchaseProcessingResult.Pending��Ԃ��A�I������PurchaseProcessingResult.Complete��Ԃ�
                // �A�C�e����t�^������ł���΁A�ȉ��̃��\�b�h���Ăяo��
                CompletePendingPurchase(purchaseEvent.purchasedProduct);

                OnPurchaseSuccessResult?.Invoke(purchaseEvent.purchasedProduct);
            }
            else
            {
                OnPurchaseFailureResult.Invoke(purchaseEvent.purchasedProduct, PurchaseFailureReason.ProductUnavailable);

                // �����ŁA�w�������A�C�e����������Ȃ������ꍇ�̏������s��
                Debug.Log($"PurchaseManager ProcessPurchase: FAIL. Unrecognized product: {purchaseEvent.purchasedProduct.definition.id}");
            }
            OnPurchaseSuccessResult = null;
            OnPurchaseFailureResult = null;
            return PurchaseProcessingResult.Complete;
        }
        catch (Exception e)
        {
            Debug.LogError($"PurchaseManager ProcessPurchase Error:{e.Message}");
            // �G���[�����������ꍇ�́A�K�؂�PurchaseProcessingResult��Ԃ�
            return PurchaseProcessingResult.Pending;
        }
    }

    public void CompletePendingPurchase(UnityEngine.Purchasing.Product product)
    {
        if(!IsInitialized)
        {
            InvokeFailureEvent(PurchaseFailureReason.Unknown,product);
            return;
        }
        if (product.transactionID == null)
        {
            InvokeFailureEvent(PurchaseFailureReason.ProductUnavailable, product);
            Debug.LogError($"CompletePendingPurchase: FAIL. No pending purchase for product {product.definition.id}");
            return;
        }

        storeController.ConfirmPendingPurchase(product);
    }

    /// <summary>
    /// �ʐM�ڑ������邩�`�F�b�N���܂��B
    /// </summary>
    /// <returns><c>true</c>�̏ꍇ�͒ʐM�ڑ�������</returns>
    private static bool hasNetworkConnection()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }


}
