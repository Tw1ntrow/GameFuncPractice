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
    

    private const string tomorrowStatus = "�������";
    private const string todayStatus = "�������";
    private const string receivedStatus = "���ς�";

    /// <summary>
    /// ���O�C���{�[�i�XUI��\��
    /// </summary>
    /// <param name="loginBonusTables"></param>
    /// <param name="userLoginBonus"></param>
    public void ViewBonus(List<LoginBonusData> loginBonusTables, UserLoginBonus userLoginBonus)
    {
        //���O�C���{�[�i�X�ꗗ��\��
        foreach (var item in loginBonusTables)
        {
            // �e���O�C���{�[�i�X�̃p�l����\��
            var itemPanel = Instantiate(itemPanelObj, bonusPanelParent);
            itemPanel.GetComponent<ItemPanel>().ViewItem(item.reward_name, item.reward_num.ToString(), item.reward_Id);

            // �ߋ��Ɏ󂯎�������O�C���{�[�i�X�̏ꍇ�A�\����ς���
            if (item.day < userLoginBonus.LastLoginDay)
            {
                ViewStatusText(receivedStatus, itemPanel.transform);

            }

            // �����󂯎��̃��O�C���{�[�i�X�̕\����ς���
            if (item.day == userLoginBonus.LastLoginDay)
            {
                ViewStatusText(todayStatus, itemPanel.transform);

                // �����󂯎�������O�C���{�[�i�X�̏���ʃE�C���h�E�ŕ\��
                // TODO:�_�C�A���O�V�X�e���ɕ�������
                var dialog = Instantiate(commonDialogObj, dialogParent);
                var dialogComponent = dialog.GetComponent<CommonDialog>();
                string title = "Login Bonus!";
                string message = $"{item.reward_name}��{item.reward_num}�l�����܂����I";
                dialogComponent.ViewDialog(title, message);

            }

            // �����󂯎��\��̃��O�C���{�[�i�X�̕\����ς���
            if (item.day == userLoginBonus.LastLoginDay + 1)
            {
                ViewStatusText(tomorrowStatus,itemPanel.transform);
            }

        }

                        

    }

    // ���O�C���{�[�i�X���̏�Ԃ��p�l����ɕ\������
    private void ViewStatusText(string text,Transform itemPanel)
    {
        Transform tomorrow = Instantiate(statusTextObj, itemPanel).transform;
        tomorrow.transform.localPosition = Vector3.zero;
        tomorrow.transform.localScale = Vector3.one;
        tomorrow.GetComponent<Text>().text = text;
    }
}
