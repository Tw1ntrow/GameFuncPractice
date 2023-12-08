using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private const string LoginDayKey = "LoginDay";
    [SerializeField]
    private LoginBonusView bonusView;

    private UserLoginBonus userLoginBonus = new UserLoginBonus(); // ���̃��[�U�[�f�[�^
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
    /// ���O�C�������X�V
    /// �{���̓T�[�o�[�ōs��
    /// </summary>
    /// <returns></returns>
    private bool UpdateLoginDay()
    {
        try
        {
            // �O��܂ł̃��O�C�������擾
            int loginDay = PlayerPrefs.GetInt(LoginDayKey, 0);

            // ���O�C������i�߂� LoginBonusTable.bonusTable.Count + 1���ڂɂȂ�����1���ڂɖ߂�
            loginDay = loginDay < MaxLoginBonusNum ? loginDay + 1 : 1;

            // �X�V�������O�C������ۑ�
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
    /// ���[�U�[�ŗL�̃��O�C���{�[�i�X�f�[�^���X�V
    /// �{���̓T�[�o�[�ōs��
    /// </summary>
    /// <returns></returns>
    private bool UpdateUserData()
    {
        try
        {
            // �O��܂ł̃��O�C�������擾
            int loginDay = PlayerPrefs.GetInt(LoginDayKey, 0);

            // ���O�C���������O�C���{�[�i�X�̍ő�����𒴂��Ă�����G���[
            if(loginDay > MaxLoginBonusNum)
            {
                Debug.LogError($"Login day ({loginDay}) exceeds max login bonus days ({MaxLoginBonusNum}). Check the login bonus table.");
                return false;
            }

            // �Y�����O�C�������̃��O�C���{�[�i�X�����݂��Ȃ��ꍇ�̓G���[
            if (!LoginBonusTable.bonusTable.Exists(bonus => bonus.day == loginDay))
            {
                Debug.LogError($"No bonus data in LoginBonusTable for day: {loginDay}");
                return false;
            }

            // �ŏI���O�C�������X�V
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
