using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// スタミナ管理クラス、本来はサーバーで行う
/// </summary>
public class StaminaManager
{
    static public UserStamina userStamina { get; private set; }
    static public Action<int, int> OnChangedStamina { get; set; }

    static private float elapsedTime = 0f;  // 経過時間を保持する変数
    // 初期化処理
    static public void Init()
    {
        Login();
    }

    /// <summary>
    /// スタミナの更新
    /// 6秒毎に1回復
    /// </summary>
    /// <returns></returns>
    static public void UpdateStamina()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= StaminaTable.RecoverTime)
        {
            AddStamina(StaminaTable.RecoverRate);  // スタミナを1回復
            elapsedTime -= StaminaTable.RecoverTime;  // 経過時間をリセット
        }
    }

    static private void Login()
    {
        // 本来はサーバーからスタミナ情報を取得
        userStamina = new UserStamina(10, 100);
        OnChangedStamina?.Invoke(userStamina.Stamina, userStamina.MaxStamina);

    }

    static public void AddStamina(int stamina)
    {
        if (stamina < 0)
        {
            Debug.LogError($"スタミナ加算の引数が不正 {stamina}");
            return;
        }

        userStamina.Stamina += stamina;
        OnChangedStamina?.Invoke(userStamina.Stamina, userStamina.MaxStamina);

    }

    static public void ConsumeStamina(int stamina)
    {
        // スタミナを消費する行動に入力値制限を入れる想定
        if (!CanConsumeStamina(stamina))
        {
            Debug.LogError("スタミナ消費不可能");
            return;
        }

        userStamina.Stamina -= stamina;
        OnChangedStamina?.Invoke(userStamina.Stamina, userStamina.MaxStamina);

    }

    static public bool CanConsumeStamina(int stamina)
    {
        return userStamina.Stamina >= stamina;
    }
}
