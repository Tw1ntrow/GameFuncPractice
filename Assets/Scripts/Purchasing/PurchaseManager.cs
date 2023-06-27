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
    /// 課金成功時のデリゲート
    /// </summary>
    public delegate void SuccessEvent(UnityEngine.Purchasing.Product product);

    /// <summary>
    /// 課金完了通知イベント
    /// </summary>
    private SuccessEvent OnPurchaseSuccessResult;

    /// <summary>
    /// 課金失敗時のデリゲート
    /// </summary>
    public delegate void FailureEvent(UnityEngine.Purchasing.Product product, PurchaseFailureReason error);

    /// <summary>
    /// 課金失敗通知イベント
    /// </summary>
    private FailureEvent OnPurchaseFailureResult;

    public static bool IsInitialized => storeController != null && storeExtensionProvider != null;

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    /// <summary>
    /// インスタンス
    /// </summary>
    private static PurchaseManager instance;

    /// <summary>
    /// シングルトン
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
            // ここで購入可能なアイテムを設定する
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
                // ここで、購入可能かどうかの判定を行う
                if (product == null && !product.availableToPurchase)
                {
                    Debug.LogError($"BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    return BuyFailureError.UnknownItem;

                }

                if(!hasNetworkConnection())
                {
                    // ネットワークエラーのログを出す
                    Debug.LogError("disconnected from network");
                    return BuyFailureError.NetworkError;
                }

                // 購入処理を開始する
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
        // サーバーからレシートの状態を取得し、購入保留中である際の処理を書く
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

    // 購入成功時の処理
    // PurchaseProcessingResult.Completeが呼ばれる前にアプリが終了した場合、次回起動時に自動でコールされる
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        try
        {
            // ここで、購入成功時の処理を行う
            if (String.Equals(purchaseEvent.purchasedProduct.definition.id, "com.example.productid", StringComparison.Ordinal))
            {
                Debug.Log("You've just purchased the product!");
                // ここで、例えばゲーム内通貨を追加するなど、具体的な購入成功時の処理を行う
                // サーバーで通信を行う際はPurchaseProcessingResult.Pendingを返し、終了次第PurchaseProcessingResult.Completeを返す
                // アイテムを付与した後であれば、以下のメソッドを呼び出す
                CompletePendingPurchase(purchaseEvent.purchasedProduct);

                OnPurchaseSuccessResult?.Invoke(purchaseEvent.purchasedProduct);
            }
            else
            {
                OnPurchaseFailureResult.Invoke(purchaseEvent.purchasedProduct, PurchaseFailureReason.ProductUnavailable);

                // ここで、購入したアイテムが見つからなかった場合の処理を行う
                Debug.Log($"PurchaseManager ProcessPurchase: FAIL. Unrecognized product: {purchaseEvent.purchasedProduct.definition.id}");
            }
            OnPurchaseSuccessResult = null;
            OnPurchaseFailureResult = null;
            return PurchaseProcessingResult.Complete;
        }
        catch (Exception e)
        {
            Debug.LogError($"PurchaseManager ProcessPurchase Error:{e.Message}");
            // エラーが発生した場合は、適切なPurchaseProcessingResultを返す
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
    /// 通信接続があるかチェックします。
    /// </summary>
    /// <returns><c>true</c>の場合は通信接続がある</returns>
    private static bool hasNetworkConnection()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }


}
