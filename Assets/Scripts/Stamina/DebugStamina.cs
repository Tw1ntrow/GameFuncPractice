using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグ用関数を纏めた
/// </summary>
public class DebugStamina : MonoBehaviour
{
    [SerializeField]
    private Button consumeButton;

    private const int CONSUME_STAMINA = 10;
    private const int ADD_STAMINA = 10;
    void Awake()
    {
        // 本来ゲームサイクルを管理するクラスで呼ぶ
        StaminaManager.Init();

        StaminaManager.OnChangedStamina += ChangeConsumeButtonEnable;
    }

    private void ChangeConsumeButtonEnable(int stamina,int maxstamina)
    {
        consumeButton.interactable = stamina < CONSUME_STAMINA ? false : true;
    }

    // 本来サーバーで呼ばれる
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
