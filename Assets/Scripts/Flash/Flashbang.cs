using UnityEngine;

public class Flashbang : MonoBehaviour
{
    [SerializeField]
    private Light flashLight;
    [SerializeField]
    private float explosionDuration = 0.1f; // �����ő勭�x�ɒB����܂�

    private float lightIntensity; // �ő勭�x
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