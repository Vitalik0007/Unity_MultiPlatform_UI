using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceManager : MonoBehaviour
{
    public static InputDeviceManager Instance { get; private set; }

    public enum InputDeviceType { KeyboardMouse, Xbox, DualShock }
    public InputDeviceType CurrentInputType { get; private set; }

    public event Action<InputDeviceType> OnInputDeviceChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DetectCurrentDevice();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Reconnected)
            DetectCurrentDevice();
    }

    //You can delete Update() method. Only for testing)))
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SetInputType(InputDeviceType.KeyboardMouse);

        if (Input.GetKeyDown(KeyCode.R))
            SetInputType(InputDeviceType.Xbox);

        if (Input.GetKeyDown(KeyCode.T))
            SetInputType(InputDeviceType.DualShock);
    }

    private void DetectCurrentDevice()
    {
        if (Gamepad.current != null)
        {
            string gamepadName = Gamepad.current.name.ToLower();

            if (gamepadName.Contains("xbox"))
                SetInputType(InputDeviceType.Xbox);
            else if (gamepadName.Contains("dualshock") || gamepadName.Contains("dualsense") || gamepadName.Contains("ps"))
                SetInputType(InputDeviceType.DualShock);
            else
                SetInputType(InputDeviceType.Xbox);
        }
        else
        {
            SetInputType(InputDeviceType.KeyboardMouse);
        }
    }

    private void SetInputType(InputDeviceType newType)
    {
        if (CurrentInputType == newType) return;

        CurrentInputType = newType;
        OnInputDeviceChanged?.Invoke(newType);
    }
}
