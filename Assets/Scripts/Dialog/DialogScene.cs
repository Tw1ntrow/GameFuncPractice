using UnityEngine;

public class DialogScene : MonoBehaviour
{
    void Start()
    {
        DialogManager.Instance.CreateDialog<CommonDialog>(new CommonDialogParameter("�^�C�g��", "�{��"));
    }
}
