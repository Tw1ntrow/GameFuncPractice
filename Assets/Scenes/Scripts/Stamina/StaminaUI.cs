using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField]
    private Text stamina;

    private void Start()
    {
        DrawStamina(StaminaManager.userStamina.Stamina, StaminaManager.userStamina.MaxStamina);
        StaminaManager.OnChangedStamina += DrawStamina;
    }

    private void DrawStamina(int stamina, int maxStamina)
    {
        this.stamina.text = $"{stamina}/{maxStamina}";
    }


}
