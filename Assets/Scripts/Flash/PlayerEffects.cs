using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] 
    private Image flashEffect;
    [SerializeField] 
    private float flashDuration = 2.0f; // éùë±éûä‘


    public void TriggerFlashEffect()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        flashEffect.gameObject.SetActive(true);
        float elapsedTime = 0;

        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            flashEffect.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), elapsedTime / flashDuration);
            yield return null;
        }

        flashEffect.gameObject.SetActive(false);
    }
}