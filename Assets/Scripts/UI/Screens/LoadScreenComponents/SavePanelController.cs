using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class SavePanelController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private float padding = 20f;
    [SerializeField] private float scrollDuration = 0.2f;

    private Tween scrollTween;
    private GameObject previousSelected;

    private void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null || selected == previousSelected || !selected.transform.IsChildOf(content))
            return;

        previousSelected = selected;

        RectTransform selectedRect = selected.GetComponent<RectTransform>();
        if (selectedRect == null) return;

        Canvas.ForceUpdateCanvases();

        Vector2 localPosition = GetLocalPositionInContent(selectedRect);
        float selectedCenterY = -localPosition.y + selectedRect.rect.height / 2f;

        float contentHeight = content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;

        float normalizedPosition = Mathf.Clamp01((selectedCenterY - viewportHeight * 0.5f + padding) / (contentHeight - viewportHeight));
        float finalPosition = 1f - normalizedPosition;

        scrollTween?.Kill();
        scrollTween = DOTween.To(() => scrollRect.verticalNormalizedPosition,
                                 value => scrollRect.verticalNormalizedPosition = value,
                                 finalPosition,
                                 scrollDuration);
    }

    private Vector2 GetLocalPositionInContent(RectTransform target)
    {
        Vector2 worldPoint = target.position;
        Vector2 localPoint = content.InverseTransformPoint(worldPoint);
        return localPoint;
    }
}
