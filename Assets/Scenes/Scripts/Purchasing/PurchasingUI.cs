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
        // ‚±‚±‚ÅAw“ü¬Œ÷‚Ìˆ—‚ğs‚¤
        Debug.Log($"Purchase successful: {product.definition.id}");
    }

    private void OnPurchaseFailure(Product product, PurchaseFailureReason failureReason)
    {
        // ‚±‚±‚ÅAw“ü¸”s‚Ìˆ—‚ğs‚¤
        Debug.LogError($"Purchase failed: {failureReason}");
    }
}