using System;

public class ButtonParameter
{
    public ButtonParameter(ButtonType type, string buttonText, Action onAction = null)
    {
        Type = type;
        ButtonText = buttonText;
        this.onAction = onAction;
    }

    public ButtonType Type { get; set; }
    public string ButtonText { get; set; }
    public Action onAction { get; set; }
}

public enum ButtonType
{
    Ok,
    Cancel,
    Yes,
    No,
    Close,
    Custom
}