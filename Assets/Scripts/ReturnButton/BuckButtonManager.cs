using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バックボタンが押された際の処理を管理する
/// </summary>
public class BackButtonManager : MonoBehaviour
{
    private Stack<IBackButtonHandler> backButtonHandlers = new Stack<IBackButtonHandler>();

    public static BackButtonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backButtonHandlers.Count > 0)
            {
                backButtonHandlers.Pop().HandleBackButton();
            }
            else
            {
                Debug.Log("No Back Operation Available");
            }
        }
    }

    public void PushBackButtonHandler(IBackButtonHandler handler)
    {
        backButtonHandlers.Push(handler);
    }

    public void PopBackButtonHandler()
    {
        backButtonHandlers.Pop();
    }
}