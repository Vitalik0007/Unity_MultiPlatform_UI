using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIElementBinding
{
    public string key;

    public Image image;
    public TextMeshProUGUI text;
    public GameObject targetGameObject;
}