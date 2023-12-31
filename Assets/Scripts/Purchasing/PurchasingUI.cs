using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private TestPurchasing testPurchasing;
    [SerializeField]
    private Button purchaseButton;

    private void Awake()
    {
        testPurchasing = FindObjectOfType<TestPurchasing>();
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    private void OnPurchaseButtonClicked()
    {
        var productInfo = testPurchasing.GetProductInfo();
        PurchaseManager.Instance.Buy(
            productInfo,
            OnPurchaseSuccess,
            OnPurchaseFailure);
    }

    private void OnPurchaseSuccess(Product product)
    {
        // ここで、購入成功時の処理を行う
        Debug.Log($"Purchase successful: {product.definition.id}");
    }

    private void OnPurchaseFailure(Product product, PurchaseFailureReason failureReason)
    {
        // ここで、購入失敗時の処理を行う
        Debug.LogError($"Purchase failed: {failureReason}");
    }
}