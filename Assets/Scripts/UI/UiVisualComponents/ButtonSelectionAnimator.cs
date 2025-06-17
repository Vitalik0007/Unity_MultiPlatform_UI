using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonSelectionAnimator : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float selectedScale = 1.1f;
    [SerializeField] private float animationDuration = 0.15f;
    [SerializeField] private Ease ease = Ease.OutBack;

    private Vector3 originalScale;
    private Tween scaleTween;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        AnimateToScale(selectedScale);
        AudioManager.Instance.PlaySound(SoundType.Button_Click2);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        AnimateToScale(1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        AnimateToScale(selectedScale);
        AudioManager.Instance.PlaySound(SoundType.Button_Click2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateToScale(1f);
    }

    private void AnimateToScale(float targetScale)
    {
        scaleTween?.Kill();
        scaleTween = transform.DOScale(originalScale * targetScale, animationDuration).SetEase(ease);
    }
}