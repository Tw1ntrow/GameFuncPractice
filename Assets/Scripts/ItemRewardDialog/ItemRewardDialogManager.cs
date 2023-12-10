using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���̕�V�_�C�A���O�𐶐�����AItemRewardDialog�͖{���_�E�����[�h����̂�MonoBehaviour�͌p�����Ȃ�
/// </summary>
public class ItemRewardDialogManager : MonoBehaviour
{
    [SerializeField]
    private ItemRewardDialog itemRewardDialogPrefab;
    [SerializeField]
    private Transform dialogParent;

    //�@�V���O���g���C���X�^���X
    public static ItemRewardDialogManager instance;

    private void Awake()
    {
        //�@�V���O���g���C���X�^���X����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //�@�C���X�^���X����������Ă���ꍇ�̓C���X�^���X��j��
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            CreateItemRewardDialog(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }

    public void CreateItemRewardDialog(List<int> items)
    {
        ItemRewardDialog dialog = Instantiate<ItemRewardDialog>(itemRewardDialogPrefab, dialogParent);
        dialog.Create(items);


    }
    
}
