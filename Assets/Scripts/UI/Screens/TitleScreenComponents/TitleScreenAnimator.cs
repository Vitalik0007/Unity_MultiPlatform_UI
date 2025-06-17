using UnityEngine;
using DG.Tweening;

public class TitleScreenAnimator : MonoBehaviour
{
    [SerializeField] private Transform pressGroupTransform;
    [SerializeField] private float pulseScale = 1.1f;
    [SerializeField] private float pulseDuration = 0.6f;

    private Tween pulseTween;

    public void PlayIntroAnimation()
    {
        StartPulse();
    }

    public void PlayExitAnimation(System.Action onComplete)
    {
        StopPulse();
        pressGroupTransform.DOScale(Vector3.zero, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() => onComplete?.Invoke());
    }

    private void StartPulse()
    {
        pressGroupTransform.localScale = Vector3.one;

        pulseTween = pressGroupTransform
            .DOScale(Vector3.one * pulseScale, pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopPulse()
    {
        pulseTween?.Kill();
        pressGroupTransform.localScale = Vector3.one;
    }
}