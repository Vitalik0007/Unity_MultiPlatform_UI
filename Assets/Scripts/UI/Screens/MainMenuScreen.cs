using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    public override UIScreenType ScreenType => UIScreenType.MainMenu;

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;

    private void Awake()
    {
        loadGameButton.onClick.AddListener(() => UIManager.Instance.OpenScreen(UIScreenType.LoadSave));
        settingsButton.onClick.AddListener(() => UIManager.Instance.OpenScreen(UIScreenType.Settings));
        creditsButton.onClick.AddListener(() => UIManager.Instance.OpenScreen(UIScreenType.Credits));
    }

    public override void Submit()
    {
        base.Submit();
    }
}