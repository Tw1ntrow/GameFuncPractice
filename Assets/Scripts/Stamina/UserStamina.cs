using UnityEngine;

public class UserStamina
{
    public UserStamina(int stamina, int maxStamina)
    {
        MaxStamina = maxStamina;
        Stamina = stamina;
    }

    public int Stamina { get { return stamina; }
        set 
        {
            value = Mathf.Clamp(value, 0, MaxStamina);
            if(stamina != value)
            {
                stamina = value;
            }
        }
    }

    private int stamina;
    // レベルアップでMaxStaminaが上がる事を考慮して初期化後もセット可能
    public int MaxStamina { get; set; }

}
