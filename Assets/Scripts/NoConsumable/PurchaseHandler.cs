using UnityEngine;
using UnityEngine.UI;

public class PurchaseHandler : MonoBehaviour
{
    [SerializeField]
    private AdManager adManager;
    [SerializeField]
    private Button videoAdButton;
    [SerializeField]
    private Button bunnerAdButton;


    private void OnEnable()
    {
        IAPManager.OnNonConsumablePurchased += HandleNonConsumablePurchase;
    }

    private void OnDisable()
    {
        IAPManager.OnNonConsumablePurchased -= HandleNonConsumablePurchase;
    }

    private void Start()
    {
        // PlayerPrefs����w����Ԃ�ǂݍ���
        bool isPurchased = PlayerPrefs.GetInt("isNonConsumablePurchased", 0) == 1;
        if (isPurchased && adManager != null)
        {
            DisableAdsPermanently();

        }
    }

    public void HandleNonConsumablePurchase()
    {
        // Non-Consumable�A�C�e���̍w��������
        PlayerPrefs.SetInt("isNonConsumablePurchased", 1);
        PlayerPrefs.Save();

        if (adManager != null)
        {
            DisableAdsPermanently();
        }
    }


    public void DisableAdsPermanently()
    {
        videoAdButton.interactable = false;
        bunnerAdButton.interactable = false;
        adManager.DisableAdsPermanently();
    }

    public void PlayerPrefsReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        videoAdButton.interactable = true;
        bunnerAdButton.interactable = true;
    }
}