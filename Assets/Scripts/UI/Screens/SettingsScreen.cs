using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
    public override UIScreenType ScreenType => UIScreenType.Settings;

    [SerializeField] private Button closeButton;

    [Header("Tabs and Pages")]
    [SerializeField] private Button[] tabButtons;
    [SerializeField] private GameObject[] pages;

    private int currentPageIndex = 0;

    private void Awake()
    {
        closeButton.onClick.AddListener(Cancel);

        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i;
            var tab = tabButtons[i];
            var eventTrigger = tab.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
                eventTrigger = tab.gameObject.AddComponent<EventTrigger>();

            var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };
            entry.callback.AddListener((data) => OnTabSelected(index));
            eventTrigger.triggers.Add(entry);
        }
    }

    public override void Show()
    {
        base.Show();
        ShowPage(0);
        SelectTabButton(0);
    }

    private void OnTabSelected(int index)
    {
        if (index == currentPageIndex) return;
        ShowPage(index);
    }

    private void ShowPage(int index)
    {
        if (index < 0 || index >= pages.Length) return;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        currentPageIndex = index;
    }

    private void SelectTabButton(int index)
    {
        if (index < 0 || index >= tabButtons.Length) return;

        EventSystem.current.SetSelectedGameObject(tabButtons[index].gameObject);
    }

    public override void Submit()
    {
        base.Submit();
    }

    public override void Cancel()
    {
        base.Cancel();

        UIManager.Instance.OpenScreen(UIScreenType.MainMenu);
    }
}
