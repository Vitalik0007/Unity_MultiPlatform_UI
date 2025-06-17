using UnityEngine;
using UnityEngine.UI;

public class LoadSaveScreen : UIScreen
{
    public override UIScreenType ScreenType => UIScreenType.LoadSave;

    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Cancel);
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
