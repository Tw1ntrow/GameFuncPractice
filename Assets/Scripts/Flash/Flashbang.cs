using UnityEngine;

public class Flashbang : MonoBehaviour
{
    [SerializeField]
    private Light flashLight;
    [SerializeField]
    private float explosionDuration = 0.1f; // 光が最大強度に達するまで

    private float lightIntensity; // 最大強度
    private float elapsedTime;

    void Start()
    {
        lightIntensity = flashLight.intensity;
        flashLight.intensity = 0;
        elapsedTime = 0;
    }

    void Update()
    {
        if (elapsedTime < explosionDuration)
        {
            elapsedTime += Time.deltaTime;
            flashLight.intensity = Mathf.Lerp(0, lightIntensity, elapsedTime / explosionDuration);
        }
        else
        {
            FindObjectOfType<PlayerEffects>().TriggerFlashEffect();
            this.enabled = false;
            flashLight.enabled = false;
        }
    }
}