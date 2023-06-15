using System.Collections.Generic;

public class LoginBonusData
{
    public int id;
    public int day;
    public string reward_name;
    public int reward_num;
    public int reward_type;
}
public class LoginBonusTable
{

    // 仮のログインボーナス
    public static List<LoginBonusData> bonusTable = new List<LoginBonusData>
    {
        new LoginBonusData { id = 1, day = 1, reward_name = "Coin", reward_num = 100, reward_type = 0 },
        new LoginBonusData { id = 2, day = 2, reward_name = "Gem", reward_num = 5, reward_type = 1 },
        new LoginBonusData { id = 3, day = 3, reward_name = "Special Item", reward_num = 1, reward_type = 2 },
        new LoginBonusData { id = 4, day = 4, reward_name = "Coin", reward_num = 200, reward_type = 0 },
        new LoginBonusData { id = 5, day = 5, reward_name = "Gem", reward_num = 10, reward_type = 1 },
        new LoginBonusData { id = 6, day = 6, reward_name = "Special Item", reward_num = 2, reward_type = 2 },
        new LoginBonusData { id = 7, day = 7, reward_name = "Super Item", reward_num = 1, reward_type = 3 },
    };


}
