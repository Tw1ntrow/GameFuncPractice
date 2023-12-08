// 汎用ダイアログ設定パラメーター
using System.Collections.Generic;

public class CommonDialogParameter : DialogParameter
{
    public string title;
    public string message;
    public List<ButtonParameter> buttonParameters = new List<ButtonParameter>();

    public CommonDialogParameter(string title, string message, List<ButtonParameter> buttonParameters = null)
    {
        this.title = title;
        this.message = message;
        this.buttonParameters = buttonParameters;
    }
}
