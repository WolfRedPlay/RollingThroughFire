using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image mtransitionImage;
    [SerializeField] private float mtransitionSpeed = 2f;

    private void Awake()
    {
        mtransitionImage = GameObject.FindWithTag("BlackImage").GetComponent<Image>();
        FadeOut();
    }   

    public void FadeFunction(bool fadeTo)
    {
        StartCoroutine(FadeAnimation(fadeTo));
    }

    private IEnumerator FadeAnimation(bool fadeIn)
    {
        if (mtransitionImage == null)
        {
            yield break;
        }

        float elapsed = 0f;
        Color startColor = mtransitionImage.color;
        float startAlpha = startColor.a;
        float targetAlpha = fadeIn ? 1f : 0f;

        while (elapsed < mtransitionSpeed)
        {
            elapsed += 0.008f;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / mtransitionSpeed);
            mtransitionImage.color = new Color(startColor.r, startColor.g, startColor.b, startAlpha);
            yield return null;
        }

        mtransitionImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }

    public void FadeIn()
    {
        FadeFunction(false);
    }

    public void FadeOut()
    {
        FadeFunction(true);
    }
}
