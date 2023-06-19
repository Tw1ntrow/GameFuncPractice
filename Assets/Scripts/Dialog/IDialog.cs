using System;
using UnityEngine;

public interface IDialog
{
    public void ViewDialog(DialogParameter parameter);
    public void CloseDialog();

    // �_�C�A���O���ŕ���{�^���������ꂽ�ꍇ�A�R�[���o�b�N��ʂ��ă}�l�[�W���[�ɂ��̃C�x���g�����s�����
    public Action<IDialog> OnClickCloseButton { get; set; }
}
