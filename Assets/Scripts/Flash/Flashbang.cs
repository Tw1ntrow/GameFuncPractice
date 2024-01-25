using UnityEngine;

public class Flashbang : MonoBehaviour
{
    [SerializeField]
    private Light flashLight;
    [SerializeField]
    private float explosionDuration = 0.1f; // Œõ‚ªÅ‘å‹­“x‚É’B‚·‚é‚Ü‚Å

    private float lightIntensity; // Å‘å‹­“x
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