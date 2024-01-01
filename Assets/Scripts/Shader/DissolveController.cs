using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField]
    private Material dissolveMaterial;
    [SerializeField]
    private float dissolveSpeed = 0.5f;

    private float threshold = 0.0f;

    void Update()
    {
        if (threshold < 1.0f)
        {
            threshold += dissolveSpeed * Time.deltaTime;
            dissolveMaterial.SetFloat("_DissolveThreshold", threshold);
        }
    }
}