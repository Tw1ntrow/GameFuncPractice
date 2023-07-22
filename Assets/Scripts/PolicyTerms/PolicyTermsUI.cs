using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyTermsUI : MonoBehaviour
{
    [SerializeField]
    private Toggle consentToggle;
    [SerializeField]
    private Text toggleContext;

    private bool isTermsOfServiceChecked = false;
    private bool isPrivacyPolicyChecked = false;
    private void Start()
    {
        isPrivacyPolicyChecked = false;
        isPrivacyPolicyChecked = false;
        consentToggle.interactable = false;
        toggleContext.enabled = true;
    }
    public void OnTermsOfServiceButton()
    {
        DialogManager.Instance.CreateDialog<WebViewDialog>(new WebViewDialogParameter("https://www.google.com", new Vector2(1000, 600)));
        isTermsOfServiceChecked = true;
        UnLockConsentToggle();
    }

    public void OnPrivacyPolicyButton()
    {
        DialogManager.Instance.CreateDialog<WebViewDialog>(new WebViewDialogParameter("https://www.google.com", new Vector2(1000, 600)));
        isPrivacyPolicyChecked = true;
        UnLockConsentToggle();

    }

    // プライバシーポリシー、利用規約をチェックした後のみチェック可能にする
    public void UnLockConsentToggle()
    {
        if (isPrivacyPolicyChecked && isPrivacyPolicyChecked)
        {
            consentToggle.interactable = true;
            toggleContext.enabled = false;
        }
    }
}
