using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreparataioUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text stateText;
    [SerializeField] TMP_Text resText;
    [SerializeField] TMP_Text coolDownText;
    [SerializeField] Transform towerSelectParent;
    [SerializeField] GameObject TowerSelectBtn;
    [SerializeField] Button stopSelectTowerBtn;
    List<GameObject> towerSelectList = new();

    int min;
    float sec;

    private void OnEnable()
    {
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
        PreparationStage.onPreparationStateChange += CheckPreparationState;
        PreparationStage.onTowerPlaced += UnSetAllSelectionButton;
    }

    private void OnDisable()
    {
        GameplayManager.instance.onResourcesChange -= CheckResourceUpdate;
        PreparationStage.onPreparationStateChange -= CheckPreparationState;
        PreparationStage.onTowerPlaced -= UnSetAllSelectionButton;

        foreach (var item in towerSelectList)
        {
            item.GetComponent<TowerSelectButton>().ClearOnClick();
        }
        towerSelectList.Clear();
    }

    public void InitiateTowerButtons(int amount, TowerDetail[] detail, UnityAction<int> onClick)
    {
        for (int i = 0; i < amount; i++)
        {
            int temp = i;
            GameObject go = Instantiate(TowerSelectBtn, towerSelectParent);
            TowerSelectButton button = go.GetComponent<TowerSelectButton>();
            button.SetOnClick(()=> onClick(temp));
            button.SetOnClick(UnSetAllSelectionButton);
            button.SetOnClick(button.SetSelected);
            button.SetResourcesText(detail[temp].costToUse);
            towerSelectList.Add(go);
        }
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
    void CheckResourceUpdate(float value)
    {
        resText.SetText($"Resource: {value}");
    }
}
