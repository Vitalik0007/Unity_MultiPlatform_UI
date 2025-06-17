using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InputHintDisplayer : MonoBehaviour
{
    [SerializeField] private InputHintSet hintSet;

    [SerializeField] private List<UIElementBinding> iconBindings;
    [SerializeField] private List<UIElementBinding> textBindings;
    [SerializeField] private List<UIElementBinding> gameObjectBindings;

    private Dictionary<string, Image> iconMap;
    private Dictionary<string, TextMeshProUGUI> textMap;
    private Dictionary<string, GameObject> gameObjectMap;

    private void Awake()
    {
        iconMap = CreateImageMap(iconBindings);
        textMap = CreateTextMap(textBindings);
        gameObjectMap = CreateGameObjectMap(gameObjectBindings);
    }

    private void OnEnable()
    {
        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance.OnInputDeviceChanged += UpdateHint;
            UpdateHint(InputDeviceManager.Instance.CurrentInputType);
        }
        else
        {
            Debug.LogWarning("InputDeviceManager.Instance is null!");
        }
    }

    private void OnDisable()
    {
        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance.OnInputDeviceChanged -= UpdateHint;
        }
    }

    public void UpdateHint(InputDeviceManager.InputDeviceType type)
    {
        UIHintElement[] hints = hintSet.keyboardMouseHints;

        if (type == InputDeviceManager.InputDeviceType.Xbox)
            hints = hintSet.xboxHints;
        else if (type == InputDeviceManager.InputDeviceType.DualShock)
            hints = hintSet.psHints;

        foreach (var hint in hints)
        {
            if (string.IsNullOrEmpty(hint.key))
                continue;

            if (iconMap.TryGetValue(hint.key, out var image))
            {
                if (hint.icon != null)
                    image.sprite = hint.icon;
            }

            if (textMap.TryGetValue(hint.key, out var textComponent))
            {
                if (!string.IsNullOrEmpty(hint.text))
                    textComponent.text = hint.text;
            }

            foreach (var setting in hint.visibilityOverrides)
            {
                switch (setting.target)
                {
                    case ElementTarget.Image:
                        if (iconMap.TryGetValue(hint.key, out var img))
                            ApplyActiveState(img.gameObject, setting.activeState);
                        break;

                    case ElementTarget.Text:
                        if (textMap.TryGetValue(hint.key, out var txt))
                            ApplyActiveState(txt.gameObject, setting.activeState);
                        break;

                    case ElementTarget.GameObject:
                        if (gameObjectMap.TryGetValue(hint.key, out var go))
                            ApplyActiveState(go, setting.activeState);
                        break;
                }
            }
        }
    }

    private void ApplyActiveState(GameObject go, ActiveState state)
    {
        switch (state)
        {
            case ActiveState.Show:
                go.SetActive(true);
                break;
            case ActiveState.Hide:
                go.SetActive(false);
                break;
            case ActiveState.DoNotChange:
                break;
        }
    }

    private Dictionary<string, Image> CreateImageMap(List<UIElementBinding> bindings)
    {
        var map = new Dictionary<string, Image>();
        foreach (var b in bindings)
            if (!string.IsNullOrEmpty(b.key) && b.image != null)
                map[b.key] = b.image;
        return map;
    }

    private Dictionary<string, TextMeshProUGUI> CreateTextMap(List<UIElementBinding> bindings)
    {
        var map = new Dictionary<string, TextMeshProUGUI>();
        foreach (var b in bindings)
            if (!string.IsNullOrEmpty(b.key) && b.text != null)
                map[b.key] = b.text;
        return map;
    }

    private Dictionary<string, GameObject> CreateGameObjectMap(List<UIElementBinding> bindings)
    {
        var map = new Dictionary<string, GameObject>();
        foreach (var b in bindings)
            if (!string.IsNullOrEmpty(b.key) && b.targetGameObject != null)
                map[b.key] = b.targetGameObject;
        return map;
    }
}