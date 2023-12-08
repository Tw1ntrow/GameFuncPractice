using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopListView : MonoBehaviour
{
    [SerializeField]
    private Transform itemPanelParent;
    [SerializeField]
    private ShopPanel shopPanel;

    private List<ShopPanel> shopPanels = new List<ShopPanel>();
    public void AddShopList(ProductItem product,bool isSoldOut, Action<int> OnClickProduct)
    {
        var panel = Instantiate<ShopPanel>(shopPanel, itemPanelParent);
        panel.DisplayPanel(product.Id, product.Name, isSoldOut, OnClickProduct);
        shopPanels.Add(panel);


    }

    public void UpdateShopList(int productId, bool isSoldOut) 
    {
        ShopPanel panel = shopPanels.Find((panel) => panel.ProductId == productId);
        panel.UpdatePanel(isSoldOut);
    }



}
