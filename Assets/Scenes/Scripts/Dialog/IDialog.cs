using System;
using UnityEngine;

public interface IDialog
{
    public void ViewDialog(DialogParameter parameter);
    public void CloseDialog();

    // ダイアログ側で閉じるボタンが押された場合、コールバックを通じてマネージャーにこのイベントが発行される
    // 外部からはこのイベントを購読することで、ダイアログの閉じるボタンが押されたことを検知できる
    public Action<IDialog> OnClickCloseButton { get; set; }
}
