using System;
using UnityEngine;

public interface IDialog
{
    public void ViewDialog(DialogParameter parameter);
    public void CloseDialog();

    // �_�C�A���O���ŕ���{�^���������ꂽ�ꍇ�A�R�[���o�b�N��ʂ��ă}�l�[�W���[�ɂ��̃C�x���g�����s�����
    // �O������͂��̃C�x���g���w�ǂ��邱�ƂŁA�_�C�A���O�̕���{�^���������ꂽ���Ƃ����m�ł���
    public Action<IDialog> OnClickCloseButton { get; set; }
}
