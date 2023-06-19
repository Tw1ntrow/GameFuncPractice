using System;
using UnityEngine;

public interface IDialog
{
    public void ViewDialog(DialogParameter parameter);
    public void CloseDialog();

    // ダイアログ側で閉じるボタンが押された場合、コールバックを通じてマネージャーにこのイベントが発行される
    public Action<IDialog> OnClickCloseButton { get; set; }
}
