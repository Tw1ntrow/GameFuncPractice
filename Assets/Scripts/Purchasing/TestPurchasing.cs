using UnityEngine;
using UnityEngine.Purchasing;

public class TestPurchasing : MonoBehaviour
{
    private PurchaseManager.ProductInfo consumableProduct;

    private void Awake()
    {
        PurchaseManager.Instance.Initialize();
        consumableProduct = new PurchaseManager.ProductInfo("com.example.productid", ProductType.Consumable);
    }

    public PurchaseManager.ProductInfo GetProductInfo()
    {
        return consumableProduct;
    }
}