using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text resourcesText;

    public void SetOnClick(UnityAction onClick)
    {
        button.onClick.AddListener(onClick);
    }

    public void ClearOnClick()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SetResourcesText(float value)
    {
        resourcesText.SetText($"{value}");
    }

    public void SetSelected()
    {
        text.SetText("SELECTED!");
    }

    public void UnSetSelected()
    {
        text.SetText("");
    }
}
