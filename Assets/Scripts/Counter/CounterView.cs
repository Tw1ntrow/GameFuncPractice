using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カウンターの見た目を更新する
/// </summary>
public class CounterView : MonoBehaviour
{
    [SerializeField]
    private Text count;

    public Action<int> OnClickedIncrementButton;
    public Action<int> OnClickedDecrementButton;
    public Action OnClickedMaxButton;
    public Action OnClickedMinButton;

    public void SetView(int countValue, Action<int> onClickedIncrementButton, Action<int> onClickedDecrementButton,
        Action onClickedMaxButton, Action onClickedMinButton)
    {
        if (count == null) throw new NullReferenceException("Count is null. Assign a Text object in inspector.");

        count.text = countValue.ToString();
        OnClickedIncrementButton = onClickedIncrementButton;
        OnClickedDecrementButton = onClickedDecrementButton;
        OnClickedMaxButton = onClickedMaxButton;
        OnClickedMinButton = onClickedMinButton;
    }

    public void SetCount(int countValue)
    {
        if (count == null) throw new NullReferenceException("Count is null. Assign a Text object in inspector.");

        count.text = countValue.ToString();
    }

    public void OnClickIncrementButton()
    {
        OnClickedIncrementButton?.Invoke(GetCount());
    }

    public void OnClickDecrementButton()
    {
        OnClickedDecrementButton?.Invoke(GetCount());
    }

    public void OnClickMaxButton()
    {
        OnClickedMaxButton?.Invoke();
    }

    public void OnClickMinButton()
    {
        OnClickedMinButton?.Invoke();
    }

    private int GetCount()
    {
        if (!int.TryParse(count.text, out int value))
        {
            throw new FormatException($"Failure to convert string to int: {count.text}");
        }

        return value;
    }
}