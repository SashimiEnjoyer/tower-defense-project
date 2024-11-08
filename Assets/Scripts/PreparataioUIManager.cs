using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreparataioUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text stateText;
    [SerializeField] TMP_Text resText;
    [SerializeField] TMP_Text resUpdText;
    [SerializeField] TMP_Text coolDownText;
    [SerializeField] Transform towerSelectParent;
    [SerializeField] GameObject TowerSelectBtn;
    [SerializeField] Button stopSelectTowerBtn;
    List<GameObject> towerSelectList = new();

    private int min;
    private float sec;
    private float lastUpdateRes = 0f;

    private void OnEnable()
    {
        GameplayManager.instance.onStateChange += CheckGameplayState;
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
        PreparationStage.onPreparationStateChange += CheckPreparationState;
        PreparationStage.onTowerPlaced += UnSetAllSelectionButton;
    }

    private void OnDisable()
    {
        GameplayManager.instance.onStateChange -= CheckGameplayState;
        GameplayManager.instance.onResourcesChange -= CheckResourceUpdate;
        PreparationStage.onPreparationStateChange -= CheckPreparationState;
        PreparationStage.onTowerPlaced -= UnSetAllSelectionButton;
    }

    public void InitiateTowerButtons(int towerIdx, TowerDetail detail, UnityAction<int> onClick)
    {
        GameObject go = Instantiate(TowerSelectBtn, towerSelectParent);
        TowerSelectButton button = go.GetComponent<TowerSelectButton>();
        button.SetOnClick(()=> onClick(towerIdx));
        button.SetOnClick(UnSetAllSelectionButton);
        button.SetOnClick(button.SetSelected);
        button.SetCardTexts(detail);
        towerSelectList.Add(go);
    }

    public void SetCoolDownText(float timer)
    {
        float result = 300 - timer;

        sec = result % 60;
        min = (int)result / 60;

        coolDownText.SetText($"Remaining: {min:F0}:{sec:F0}");
    }

    public void UnSetAllSelectionButton()
    {
        foreach (var item in towerSelectList)
        {
            item.GetComponent<TowerSelectButton>().UnSetSelected();
        }
    }

    void CheckPreparationState(PlacementState state)
    {
        switch (state)
        {
            case PlacementState.Place:
                stateText.SetText("Place Tower");
                break;
            case PlacementState.Delete:
                stateText.SetText("Delete Tower");
                break;
        }
    }

    void CheckGameplayState(GameplayState state)
    {
        switch (state)
        {
            case GameplayState.Day:
                break;
            case GameplayState.Night:
                UnSetAllSelectionButton();
                break;
        }
    }

    void CheckResourceUpdate(float value)
    {
        resText.SetText($"Resource: {value}");

        resUpdText.rectTransform.position = new Vector2(246, resUpdText.rectTransform.position.y);
        resUpdText.gameObject.SetActive(false);
        DOTween.Kill(resUpdText);

        if (lastUpdateRes > 0f)
        {
            resUpdText.SetText($"- {Mathf.Abs(lastUpdateRes - value):F0}");
            resUpdText.gameObject.SetActive(true);
            resUpdText.rectTransform.DOMoveX(360f, 1f).OnComplete(() =>
            {
                resUpdText.rectTransform.position = new Vector2(246, resUpdText.rectTransform.position.y);
                resUpdText.gameObject.SetActive(false);
            });
        }

        lastUpdateRes = value;
    }
}
