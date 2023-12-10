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
    // ���x���A�b�v��MaxStamina���オ�鎖���l�����ď���������Z�b�g�\
    public int MaxStamina { get; set; }

}
