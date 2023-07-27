using UnityEngine;

public class CursorEffectSystem : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem effectPrefab;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            ParticleSystem effectInstance = Instantiate(effectPrefab, worldPos, Quaternion.identity);
            effectInstance.Play();
        }
    }
}