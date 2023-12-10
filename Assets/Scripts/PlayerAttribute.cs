using System;
using UnityEngine;

public class PlayerAttribute
{
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                hp = value;
                OnChangedHP?.Invoke(hp);
            }
        }
    }

    public int MaxHP { get; set; }

    public Action<int> OnChangedHP;
}