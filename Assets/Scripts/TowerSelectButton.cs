using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject selectIndicator;
    [SerializeField] TMP_Text text;    
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text intervalAtkText;
    [SerializeField] TMP_Text projectileSpeed;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text resourcesText;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rect.DOMoveY(24, .5f);
    }

    private void OnDisable()
    {
        rect.position = new Vector3(rect.position.x, -300);
    }

    public void SetOnClick(UnityAction onClick)
    {
        button.onClick.AddListener(onClick);
    }

    public void ClearOnClick()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SetCardTexts(TowerDetail detail)
    {
        text.SetText($"HP: {detail.name}");
        hpText.SetText($"HP: {detail.maxHealth}");
        damageText.SetText($"Damage: {detail.damage}");
        intervalAtkText.SetText($"Interval ATK: {detail.intervalShoot}");
        projectileSpeed.SetText($"Projectile SPD: {detail.projectileSpeed}");
        descriptionText.SetText($"{detail.description}");
        resourcesText.SetText($"Cost: {detail.costToUse}");
    }

    public void SetSelected()
    {
        selectIndicator.SetActive(true);
    }

    public void UnSetSelected()
    {
        selectIndicator.SetActive(false);
    }
}
