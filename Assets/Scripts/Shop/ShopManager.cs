using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ShopListView shopListView;
    [SerializeField]
    private ProductDialog productDialog;
    [SerializeField]
    private CommonDialog commonDialog;

    void Start()
    {
        // �e�X�g�p�ɏ�������1000�ɂ���
        UserCurrency.AddCurrency(CurrencyManager.GoldInstance, 1000);

        // ���i���X�g���쐬
        foreach (var product in ProductTable.TestTable)
        {
            shopListView.AddShopList(product, IsSoldOut(product), DisplayProductDialog);
        }

    }
    // ���i�̍݌ɐ���0�Ń��[�U�[�̎����Ă���ʉ݁i�w�肳�ꂽ��ނ̒ʉ݁j�����i�̉��i�������Ȃ��ꍇ�͔̔��s�Ƃ���
    private bool IsSoldOut(IPurchasable product)
    {
        // �݌ɂ�-1�̏ꍇ�͏�ɔ���؂�Ă���Ƃ݂Ȃ�
        if (product.Stock == 0)
        {
            return true;
        }

        return product.Price > UserCurrency.GetBalance(product.CurrencyType);
    }

    // ���i�p�l���������ꂽ�ہA���ʑI���_�C�A���O��\��
    public void DisplayProductDialog(int ProductId)
    {
        ProductDialog dialog = Instantiate<ProductDialog>(productDialog, transform);
        IPurchasable product = ProductTable.TestTable.Find((product) => product.Id == ProductId);
        dialog.DisplayDialog(ProductId, product.Price, product.CurrencyType.Name,
            GetMaxPurchaseQuantity(product), OnSucsessPurchase);

    }

    // �w���\�ȍő吔���擾����
    private int GetMaxPurchaseQuantity(IPurchasable product)
    {
        int maxQuantityByBalance = UserCurrency.GetBalance(product.CurrencyType) / product.Price;

        if (product.Stock == -1) // �݌ɂ�-1�̏ꍇ�͏�ɔ���؂�Ă���Ƃ݂Ȃ�
        {
            return maxQuantityByBalance;
        }
        else
        {
            return Math.Min(maxQuantityByBalance, product.Stock);
        }
    }

    // �w���������̏���
    private void OnSucsessPurchase(int productId, int quantity)
    {
        // �w���������̃_�C�A���O��\��
        CommonDialog dialog = Instantiate<CommonDialog>(commonDialog, transform);
        dialog.ViewDialog("�w������", $"���iID:{productId}��{quantity}�w�����܂����B");

        // ���ۂɍw���������i���C�x���g���Ɋi�[
        IPurchasable product = ProductTable.TestTable.Find((product) => product.Id == productId);
        product.OnPurchase(quantity);

        // �w����̍݌ɐ��ɉ����āA���i�p�l���̔���؂�\�����X�V
        foreach (var productTable in ProductTable.TestTable)
        {
            shopListView.UpdateShopList(productTable.Id, IsSoldOut(productTable));
        }

    }

}
