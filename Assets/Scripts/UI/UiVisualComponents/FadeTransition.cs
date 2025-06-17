using UnityEngine;
using DG.Tweening;
using System;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeOverlay;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (fadeOverlay == null)
        {
            Debug.LogError("FadeTransition: CanvasGroup is not assigned!");
            enabled = false;
        }
        fadeOverlay.alpha = 0f;
        fadeOverlay.gameObject.SetActive(false);
    }

    public void FadeToScreen(Action switchScreenAction, Action onComplete = null)
    {
        fadeOverlay.gameObject.SetActive(true);
        fadeOverlay.alpha = 0f;

        fadeOverlay.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            switchScreenAction?.Invoke();

            fadeOverlay.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                fadeOverlay.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        });
    }
}
