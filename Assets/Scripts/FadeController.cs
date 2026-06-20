using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(0f);
    }

    public IEnumerator Fade(float duration)
    {
        fadeImage.CrossFadeAlpha(1f,0.25f, false);
        yield return new WaitForSeconds(duration);

        fadeImage.CrossFadeAlpha(0f, 0.25f, false);
    }
}