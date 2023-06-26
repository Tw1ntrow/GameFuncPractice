using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// �X�^�~�i�Ǘ��N���X�A�{���̓T�[�o�[�ōs��
/// </summary>
public class StaminaManager
{
    static public UserStamina userStamina { get; private set; }
    static public Action<int, int> OnChangedStamina { get; set; }

    static private float elapsedTime = 0f;  // �o�ߎ��Ԃ�ێ�����ϐ�
    // ����������
    static public void Init()
    {
        Login();
    }

    /// <summary>
    /// �X�^�~�i�̍X�V
    /// 6�b����1��
    /// </summary>
    /// <returns></returns>
    static public void UpdateStamina()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= StaminaTable.RecoverTime)
        {
            AddStamina(StaminaTable.RecoverRate);  // �X�^�~�i��1��
            elapsedTime -= StaminaTable.RecoverTime;  // �o�ߎ��Ԃ����Z�b�g
        }
    }

    static private void Login()
    {
        // �{���̓T�[�o�[����X�^�~�i�����擾
        userStamina = new UserStamina(10, 100);
        OnChangedStamina?.Invoke(userStamina.Stamina, userStamina.MaxStamina);

    }

    static public void AddStamina(int stamina)
    {
        if (stamina < 0)
        {
            Debug.LogError($"�X�^�~�i���Z�̈������s�� {stamina}");
            return;
        }

        userStamina.Stamina += stamina;
        OnChangedStamina?.Invoke(userStamina.Stamina, userStamina.MaxStamina);

    }

    static public void ConsumeStamina(int stamina)
    {
        // �X�^�~�i�������s���ɓ��͒l����������z��
        if (!CanConsumeStamina(stamina))
        {
            Debug.LogError("�X�^�~�i����s�\");
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
