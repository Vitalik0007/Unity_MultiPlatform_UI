using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollDuration = 20f;
    [SerializeField] private float restartDelay = 2f;

    private Tween scrollTween;

    private void Start()
    {
        StartScrolling();
    }

    private void StartScrolling()
    {
        scrollRect.verticalNormalizedPosition = 1f;

        scrollTween = DOTween.To(
            () => scrollRect.verticalNormalizedPosition,
            value => scrollRect.verticalNormalizedPosition = value,
            0f,
            scrollDuration
        )
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            DOVirtual.DelayedCall(restartDelay, StartScrolling);
        });
    }

    private void OnDisable()
    {
        scrollTween?.Kill();
    }

    private void OnDestroy()
    {
        scrollTween?.Kill();
    }
}
