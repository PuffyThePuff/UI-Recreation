using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Echo : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image highlight;

    public void Initialize(Sprite sprite)
    {
        image.sprite = sprite;
        string echoName = sprite.name.Replace("_", " ");
        gameObject.name = echoName;
    }

    public Image GetHighlight()
    {
        return highlight;
    }

    // switches highlight opacity
    public void SwitchHighlight()
    {
        if (!highlight) return;

        StartCoroutine(FadeHighlight());
    }

    // immediately switches highlight opacity
    public void SwitchHighlightFast()
    {
        highlight.SetAlpha(highlight.color.a != 0.0f ? 0.0f : 0.3f);
    }

    IEnumerator FadeHighlight()
    {
        yield return new WaitForSeconds(0.1f);
        highlight.DOFade(highlight.color.a != 0.0f ? 0.0f : 0.3f, 0.4f);
    }
}
