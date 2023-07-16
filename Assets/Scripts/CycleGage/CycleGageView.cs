using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleGageView : MonoBehaviour
{
    [SerializeField]
    private Image gage;
    [SerializeField]
    private float cycleTime = 1.0f;

    void Start()
    {
        StartCycle(0.5f);
    }

    public void StartCycle(float hp)
    {
        StartCoroutine(Cycle(hp));
    }

    public IEnumerator Cycle(float targetHP)
    {
        float startHP = gage.fillAmount;
        float elapsed = 0.0f;

        while (elapsed < cycleTime)
        {
            elapsed += Time.deltaTime;
            float norTime = elapsed / cycleTime;
            gage.fillAmount = Mathf.Lerp(startHP, targetHP, norTime);
            yield return null;
        }

        gage.fillAmount = targetHP;
    }
}
