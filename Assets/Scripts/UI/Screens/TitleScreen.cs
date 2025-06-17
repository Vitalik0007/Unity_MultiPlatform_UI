using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : UIScreen
{
    public override UIScreenType ScreenType => UIScreenType.Title;
    [SerializeField] private Button submitButton;
    [SerializeField] private TitleScreenAnimator animator;
    [SerializeField] private FadeTransition fadeTransition;

    private void Awake()
    {
        submitButton.onClick.AddListener(Submit);
    }

    private void OnEnable()
    {
        animator.PlayIntroAnimation();
    }

    public override void Submit()
    {
        base.Submit();

        submitButton.interactable = false;
        animator.PlayExitAnimation(() =>
        {
            fadeTransition.FadeToScreen(() => {
                UIManager.Instance.OpenScreen(UIScreenType.MainMenu);
            });
        });
    }
}