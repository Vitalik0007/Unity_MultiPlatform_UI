using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : UIScreen
{
    public override UIScreenType ScreenType => UIScreenType.Credits;

    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Cancel);
    }

    public override void Submit() => base.Submit();

    public override void Cancel()
    {
        base.Cancel();

        UIManager.Instance.OpenScreen(UIScreenType.MainMenu);
    }
}
