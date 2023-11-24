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
    /// 購入確定時の処理
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

        // 購入失敗時のエラー内容を表示
        switch (failureReason)
        {
            // 購入機能が利用できない場合（ネットワークの問題など）
            case PurchaseFailureReason.PurchasingUnavailable:
                failureMessage += " - Purchasing is unavailable. Please check your network connection.";
                break;
            case PurchaseFailureReason.ExistingPurchasePending:// 既に進行中の別の購入がある場合
                failureMessage += " - There is a pending purchase already in progress.";
                break;
            case PurchaseFailureReason.ProductUnavailable:// 製品が利用不可能な場合（地域制限や販売終了など）
                failureMessage += " - The product is not available.";
                break;
            case PurchaseFailureReason.SignatureInvalid: // 購入の署名が無効な場合
                failureMessage += " - The purchase signature is invalid.";
                break;
            case PurchaseFailureReason.UserCancelled:// ユーザーによる購入のキャンセル
                failureMessage += " - The user cancelled the purchase.";
                break;
            case PurchaseFailureReason.PaymentDeclined:// 支払いが拒否された場合（クレジットカードの問題など）
                failureMessage += " - Payment was declined.";
                break;
            case PurchaseFailureReason.DuplicateTransaction:// 重複したトランザクションの場合
                failureMessage += " - This is a duplicate transaction.";
                break;
            case PurchaseFailureReason.Unknown:// その他のエラー
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
                // アプリがストアに認識されていない場合
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                // 購入機能が利用不可の場合
                break;
            case InitializationFailureReason.NoProductsAvailable:
                // 購入可能な製品がない場合
                break;
        }
    }
////////////////////////////////////購入処理

    // サブスクリプション購入
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

    // 消費アイテム購入
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

        // 製品が購入可能かどうかのチェック
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
            // 指定された製品IDが見つからない場合
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