using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ʉ�UI
/// �ʉ݃N���X���O���[�o���ɂ��A�ǂ�����ł��Ăяo����悤�ɂȂ��Ă���
/// </summary>
public class CurrencyUI : MonoBehaviour
{
    [SerializeField]
    private Text gold;
    void Awake()
    {
        UserCurrency.OnCurrencyChanged += DebugGold;
        DrawGold();
    }

    private void DebugGold(ICurrency currency, int amout)
    {
        if(currency is Gold)
        {
            DrawGold();
        }
    }

    private void DrawGold()
    {
        gold.text = $"{CurrencyManager.GoldInstance.Name}:{UserCurrency.GetBalance(CurrencyManager.GoldInstance).ToString()}";

    }
}
