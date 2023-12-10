using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �f�o�b�O�p�֐���Z�߂�
/// </summary>
public class DebugStamina : MonoBehaviour
{
    [SerializeField]
    private Button consumeButton;

    private const int CONSUME_STAMINA = 10;
    private const int ADD_STAMINA = 10;
    void Awake()
    {
        // �{���Q�[���T�C�N�����Ǘ�����N���X�ŌĂ�
        StaminaManager.Init();

        StaminaManager.OnChangedStamina += ChangeConsumeButtonEnable;
    }

    private void ChangeConsumeButtonEnable(int stamina,int maxstamina)
    {
        consumeButton.interactable = stamina < CONSUME_STAMINA ? false : true;
    }

    // �{���T�[�o�[�ŌĂ΂��
    void Update()
    {
        StaminaManager.UpdateStamina();
    }

    public void AddStamina()
    {
        StaminaManager.AddStamina(ADD_STAMINA);
    }

    public void ConsumeStamina()
    {
        StaminaManager.ConsumeStamina(CONSUME_STAMINA);
    }
}
