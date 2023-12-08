using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通貨UI
/// 通貨クラスをグローバルにし、どこからでも呼び出せるようになっている
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
