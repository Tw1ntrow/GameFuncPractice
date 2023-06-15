using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private const string LoginDayKey = "LoginDay";
    [SerializeField]
    private LoginBonusView bonusView;

    private UserLoginBonus userLoginBonus = new UserLoginBonus(); // 仮のユーザーデータ
    private const int MaxLoginBonusNum = 7;

    void Start()
    {
        if (!UpdateLoginDay())
        {
            Debug.LogError("Failed to update login day.");
            return;
        }
        if (!UpdateUserData())
        {
            Debug.LogError("Failed to update user data.");
            return;
        }

        bonusView.ViewBonus(LoginBonusTable.bonusTable, userLoginBonus);
    }

    /// <summary>
    /// ログイン日数更新
    /// 本来はサーバーで行う
    /// </summary>
    /// <returns></returns>
    private bool UpdateLoginDay()
    {
        try
        {
            // 前回までのログイン日を取得
            int loginDay = PlayerPrefs.GetInt(LoginDayKey, 0);

            // ログイン日を進める LoginBonusTable.bonusTable.Count + 1日目になったら1日目に戻す
            loginDay = loginDay < MaxLoginBonusNum ? loginDay + 1 : 1;

            // 更新したログイン日を保存
            PlayerPrefs.SetInt(LoginDayKey, loginDay);
            PlayerPrefs.Save();
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"UpdateLoginDay failed with error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// ユーザー固有のログインボーナスデータを更新
    /// 本来はサーバーで行う
    /// </summary>
    /// <returns></returns>
    private bool UpdateUserData()
    {
        try
        {
            // 前回までのログイン日を取得
            int loginDay = PlayerPrefs.GetInt(LoginDayKey, 0);

            // ログイン日がログインボーナスの最大日数を超えていたらエラー
            if(loginDay > MaxLoginBonusNum)
            {
                Debug.LogError($"Login day ({loginDay}) exceeds max login bonus days ({MaxLoginBonusNum}). Check the login bonus table.");
                return false;
            }

            // 該当ログイン日数のログインボーナスが存在しない場合はエラー
            if (!LoginBonusTable.bonusTable.Exists(bonus => bonus.day == loginDay))
            {
                Debug.LogError($"No bonus data in LoginBonusTable for day: {loginDay}");
                return false;
            }

            // 最終ログイン日を更新
            userLoginBonus.LastLoginDay = loginDay;

            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"UpdateUserData failed with error: {ex.Message}");
            return false;
        }
    }


}
