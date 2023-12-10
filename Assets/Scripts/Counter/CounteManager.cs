using System;
using UnityEngine;

/// <summary>
///  カウンターの数字を制御する
/// </summary>
public class CounterManager : MonoBehaviour
{
    public class CounterViewData
    {
        public int InitCount;
        public int MaxCount;
        public int MinCount;
        public Action<int> OnChangedCount;
        public CounterViewData(int initCount, int maxCount, int minCount, Action<int> onChangedCount)
        {
            InitCount = initCount;
            MaxCount = maxCount;
            MinCount = minCount;
            OnChangedCount = onChangedCount;
        }
    }

    [SerializeField]
    private CounterView counterView;
    private CounterViewData viewData;

    private void Start()
    {
        // テスト用
        Create(new CounterViewData(0, 10, 0, (count) => Debug.Log($"Count: {count}")));
    }

    public void Create(CounterViewData viewData)
    {
        this.viewData = viewData;
        counterView.SetView(viewData.InitCount, OnIncrement, OnDecrement, OnMax, OnMin);
    }

    public void OnIncrement(int count)
    {
        int newCount = Math.Min(count + 1, viewData.MaxCount);
        UpdateCounter(newCount);
    }

    public void OnDecrement(int count)
    {
        int newCount = Math.Max(count - 1, viewData.MinCount);
        UpdateCounter(newCount);
    }

    public void OnMax()
    {
        UpdateCounter(viewData.MaxCount);
    }

    public void OnMin()
    {
        UpdateCounter(viewData.MinCount);
    }

    private void UpdateCounter(int newCount)
    {
        counterView.SetCount(newCount);
        viewData.OnChangedCount?.Invoke(newCount);
    }
}