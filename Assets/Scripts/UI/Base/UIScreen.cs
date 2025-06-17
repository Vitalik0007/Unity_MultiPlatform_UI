using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    private GameObject lastSelected;

    public abstract UIScreenType ScreenType { get; }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(SelectWithDelay());
        OnOpened();
    }

    private IEnumerator SelectWithDelay()
    {
        yield return null;
        GameObject toSelect = lastSelected != null ? lastSelected : firstSelected;
        EventSystem.current?.SetSelectedGameObject(toSelect);
    }

    public virtual void Hide()
    {
        SaveCurrentSelection();
        gameObject.SetActive(false);
    }

    protected virtual void SaveCurrentSelection()
    {
        GameObject selected = EventSystem.current?.currentSelectedGameObject;
        if (selected != null && selected.transform.IsChildOf(transform))
        {
            lastSelected = selected;
        }
    }

    protected virtual void OnOpened() { }
    public virtual void Submit() => AudioManager.Instance.PlaySound(SoundType.Button_Click1);
    public virtual void Cancel() => AudioManager.Instance.PlaySound(SoundType.Button_Click3);
}