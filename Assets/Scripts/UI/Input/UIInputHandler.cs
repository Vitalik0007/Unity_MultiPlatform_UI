using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.UI.Submit.performed += _ => Submit();
        inputActions.UI.Cancel.performed += _ => Cancel();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Submit()
    {
        UIManager.Instance.CurrentScreen?.Submit();
    }

    private void Cancel()
    {
        UIManager.Instance.CurrentScreen?.Cancel();
    }
}