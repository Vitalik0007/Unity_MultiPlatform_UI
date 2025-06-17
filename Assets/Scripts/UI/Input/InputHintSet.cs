using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Input Hint Set")]
public class InputHintSet : ScriptableObject
{
    public UIHintElement[] keyboardMouseHints;
    public UIHintElement[] xboxHints;
    public UIHintElement[] psHints;
}

[System.Serializable]
public class UIHintElement
{
    public string key;
    public Sprite icon;
    public string text;

    public List<VisibilitySetting> visibilityOverrides = new();
}

[System.Serializable]
public class VisibilitySetting
{
    public ElementTarget target;
    public ActiveState activeState = ActiveState.DoNotChange;
}