using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public static event Action OnNonConsumablePurchased;

    [SerializeField]
    private string consumableProductId = "com.company.gamename.product1";
    [SerializeField]
    private string subscriptionProductId = "com.company.gamename.subscription1";
    [SerializeField]
    private string nonConsumableProductId = "com.company.gamename.nonconsumable1";

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        try
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            if (string.IsNullOrEmpty(consumableProductId))
            {
                Debug.LogError("Consumable product ID is not set or empty.");
                return;
            }
            builder.AddProduct(consumableProductId, ProductType.Consumable);
            builder.AddProduct(subscriptionProductId, ProductType.Subscription);
            builder.AddProduct(nonConsumableProductId, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }
        catch (Exception e)
        {
            Debug.LogError($"InitializePurchasing Exception: {e.Message}");
        }
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyConsumable()
    {
        try
        {
            BuyProductID(consumableProductId);
        }
        catch (Exception e)
        {
            Debug.LogError($"BuyConsumable Exception: {e.Message}");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"OnInitializeFailed InitializationFailureReason:{error}");
    }

    /// <summary>
    /// �w���m�莞�̏���
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        try
        {
            if (String.Equals(args.purchasedProduct.definition.id, consumableProductId, StringComparison.Ordinal))
            {
                Debug.Log($"ProcessPurchase: PASS. Non-Consumable Product: '{args.purchasedProduct.definition.id}'");
            }
            else if (String.Equals(args.purchasedProduct.definition.id, subscriptionProductId, StringComparison.Ordinal))
            {
                Debug.Log($"ProcessPurchase: PASS. Subscription Product: '{args.purchasedProduct.definition.id}'");
            }
            else if (String.Equals(args.purchasedProduct.definition.id, nonConsumableProductId, StringComparison.Ordinal))
            {
                Debug.Log($"ProcessPurchase: PASS. Non-Consumable Product: '{args.purchasedProduct.definition.id}'");
                OnNonConsumablePurchased?.Invoke();
            }
            else
            {
                Debug.LogError($"ProcessPurchase: FAIL. Unrecognized product: '{args.purchasedProduct.definition.id}'");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"ProcessPurchase Exception: {e.Message}");
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        string failureMessage = $"OnPurchaseFailed: FAIL. Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}";

        // �w�����s���̃G���[���e��\��
        switch (failureReason)
        {
            // �w���@�\�����p�ł��Ȃ��ꍇ�i�l�b�g���[�N�̖��Ȃǁj
            case PurchaseFailureReason.PurchasingUnavailable:
                failureMessage += " - Purchasing is unavailable. Please check your network connection.";
                break;
            case PurchaseFailureReason.ExistingPurchasePending:// ���ɐi�s���̕ʂ̍w��������ꍇ
                failureMessage += " - There is a pending purchase already in progress.";
                break;
            case PurchaseFailureReason.ProductUnavailable:// ���i�����p�s�\�ȏꍇ�i�n�搧����̔��I���Ȃǁj
                failureMessage += " - The product is not available.";
                break;
            case PurchaseFailureReason.SignatureInvalid: // �w���̏����������ȏꍇ
                failureMessage += " - The purchase signature is invalid.";
                break;
            case PurchaseFailureReason.UserCancelled:// ���[�U�[�ɂ��w���̃L�����Z��
                failureMessage += " - The user cancelled the purchase.";
                break;
            case PurchaseFailureReason.PaymentDeclined:// �x���������ۂ��ꂽ�ꍇ�i�N���W�b�g�J�[�h�̖��Ȃǁj
                failureMessage += " - Payment was declined.";
                break;
            case PurchaseFailureReason.DuplicateTransaction:// �d�������g�����U�N�V�����̏ꍇ
                failureMessage += " - This is a duplicate transaction.";
                break;
            case PurchaseFailureReason.Unknown:// ���̑��̃G���[
                failureMessage += " - Unknown error occurred.";
                break;
        }

        Debug.LogError(failureMessage);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        string errorMessage = $"IAP Initialization Failed: {error}. Details: {message}";
        Debug.LogError(errorMessage);

        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                // �A�v�����X�g�A�ɔF������Ă��Ȃ��ꍇ
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                // �w���@�\�����p�s�̏ꍇ
                break;
            case InitializationFailureReason.NoProductsAvailable:
                // �w���\�Ȑ��i���Ȃ��ꍇ
                break;
        }
    }
////////////////////////////////////�w������

    // �T�u�X�N���v�V�����w��
    public void BuySubscription()
    {
        try
        {
            BuyProductID(subscriptionProductId);
        }
        catch (Exception e)
        {
            Debug.LogError($"BuySubscription Exception: {e.Message}");
        }
    }

    // ����A�C�e���w��
    void BuyProductID(string productId)
    {
        if (!IsInitialized())
        {
            Debug.LogError("BuyProductID FAIL. Not initialized.");
            return;
        }

        if (string.IsNullOrEmpty(productId))
        {
            Debug.LogError("Product ID is null or empty.");
            return;
        }

        Product product = m_StoreController.products.WithID(productId);

        // ���i���w���\���ǂ����̃`�F�b�N
        if (product != null)
        {
            if (product.availableToPurchase)
            {
                Debug.Log($"Purchasing product asychronously: '{product.definition.id}'");
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.LogError($"Product '{product.definition.id}' is not available for purchase.");
            }
        }
        else
        {
            // �w�肳�ꂽ���iID��������Ȃ��ꍇ
            Debug.LogError($"Product ID '{productId}' not found.");
        }
    }


    public void BuyNonConsumable()
    {
        try
        {
            BuyProductID(nonConsumableProductId);
        }
        catch (Exception e)
        {
            Debug.LogError($"BuyNonConsumable Exception: {e.Message}");
        }
    }
}