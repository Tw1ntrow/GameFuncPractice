using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private Material fadeMaterial; 
    [SerializeField]
    private float fadeDuration = 1.0f; 

    private float currentFadeTime;
    private bool isFadingIn;

    void Start()
    {
        if (fadeMaterial != null)
        {
            fadeMaterial.SetFloat("_Alpha", 0.0f);
            isFadingIn = true; // true: フェードイン, false: フェードアウト
        }
    }

    void Update()
    {
        if (fadeMaterial != null && isFadingIn)
        {
            currentFadeTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentFadeTime / fadeDuration);
            fadeMaterial.SetFloat("_Alpha", alpha);

            if (alpha >= 1.0f)
            {
                isFadingIn = false; 
            }
        }
        else if (fadeMaterial != null && !isFadingIn)
        {
            currentFadeTime -= Time.deltaTime;
            float alpha = Mathf.Clamp01(currentFadeTime / fadeDuration);
            fadeMaterial.SetFloat("_Alpha", alpha);

            if (alpha <= 0.0f)
            {
                isFadingIn = true;
            }
        }
    }
}