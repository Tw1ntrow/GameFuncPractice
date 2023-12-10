using System.Collections.Generic;
using System;

/// <summary>
/// ユーザーの所有通貨を管理する
/// </summary>
public class UserCurrency
{
    private static Dictionary<ICurrency, int> currencyBalances = new Dictionary<ICurrency, int>();

    public static event Action<ICurrency, int> OnCurrencyChanged;


    public static void AddCurrency(ICurrency currency, int amount)
    {
        if (!currencyBalances.ContainsKey(currency))
        {
            currencyBalances[currency] = 0;
        }
        currencyBalances[currency] += amount;

        OnCurrencyChanged?.Invoke(currency, currencyBalances[currency]);
    }

    public static bool SpendCurrency(ICurrency currency, int amount)
    {
        if (!currencyBalances.ContainsKey(currency) || currencyBalances[currency] < amount)
        {
            return false;
        }
        currencyBalances[currency] -= amount;

        OnCurrencyChanged?.Invoke(currency, currencyBalances[currency]);
        return true;
    }

    public static int GetBalance(ICurrency currency)
    {
        if (!currencyBalances.ContainsKey(currency))
        {
            return 0;
        }
        return currencyBalances[currency];
    }
}
