using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBonusView : MonoBehaviour
{
    [SerializeField]
    private Transform bonusPanelParent;
    [SerializeField]
    private GameObject itemPanelObj;
    [SerializeField]
    private GameObject statusTextObj;

    [SerializeField]
    private Transform dialogParent;

    [SerializeField]
    private GameObject commonDialogObj;
    

    private const string tomorrowStatus = "明日受取";
    private const string todayStatus = "今日受取";
    private const string receivedStatus = "受取済み";

    /// <summary>
    /// ログインボーナスUIを表示
    /// </summary>
    /// <param name="loginBonusTables"></param>
    /// <param name="userLoginBonus"></param>
    public void ViewBonus(List<LoginBonusData> loginBonusTables, UserLoginBonus userLoginBonus)
    {
        //ログインボーナス一覧を表示
        foreach (var item in loginBonusTables)
        {
            // 各ログインボーナスのパネルを表示
            var itemPanel = Instantiate(itemPanelObj, bonusPanelParent);
            itemPanel.GetComponent<ItemPanel>().ViewItem(item.reward_name, item.reward_num.ToString(), item.reward_Id);

            // 過去に受け取ったログインボーナスの場合、表示を変える
            if (item.day < userLoginBonus.LastLoginDay)
            {
                ViewStatusText(receivedStatus, itemPanel.transform);

            }

            // 今日受け取りのログインボーナスの表示を変える
            if (item.day == userLoginBonus.LastLoginDay)
            {
                ViewStatusText(todayStatus, itemPanel.transform);

                // 今日受け取ったログインボーナスの情報を別ウインドウで表示
                // TODO:ダイアログシステムに分離する
                var dialog = Instantiate(commonDialogObj, dialogParent);
                var dialogComponent = dialog.GetComponent<CommonDialog>();
                string title = "Login Bonus!";
                string message = $"{item.reward_name}を{item.reward_num}個獲得しました！";
                dialogComponent.ViewDialog(title, message);

            }

            // 明日受け取り予定のログインボーナスの表示を変える
            if (item.day == userLoginBonus.LastLoginDay + 1)
            {
                ViewStatusText(tomorrowStatus,itemPanel.transform);
            }

        }

                        

    }

    // ログインボーナス毎の状態をパネル上に表示する
    private void ViewStatusText(string text,Transform itemPanel)
    {
        Transform tomorrow = Instantiate(statusTextObj, itemPanel).transform;
        tomorrow.transform.localPosition = Vector3.zero;
        tomorrow.transform.localScale = Vector3.one;
        tomorrow.GetComponent<Text>().text = text;
    }
}
