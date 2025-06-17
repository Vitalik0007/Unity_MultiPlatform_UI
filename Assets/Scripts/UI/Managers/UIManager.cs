using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIScreen> screens;

    private Dictionary<UIScreenType, UIScreen> screenMap;
    public UIScreen CurrentScreen { get; private set; }

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        screenMap = new Dictionary<UIScreenType, UIScreen>();
        foreach (var screen in screens)
        {
            screen.Hide();
            screenMap.Add(screen.ScreenType, screen);
        }

        OpenScreen(UIScreenType.Title);
    }

    public void OpenScreen(UIScreenType type)
    {
        if (screenMap.TryGetValue(type, out var screen))
        {
            CurrentScreen?.Hide();
            CurrentScreen = screen;
            CurrentScreen.Show();
        }
        else
        {
            Debug.LogWarning($"No screen of type {type} registered in UIManager.");
        }
    }
}