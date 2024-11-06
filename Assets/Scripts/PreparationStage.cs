using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public enum PlacementState
{
    Place, Delete
}

[System.Serializable]
public struct PlacementDetail
{
    public Transform placement;
    public GameObject currentTower;
}

public class PreparationStage : MonoBehaviour
{
    private PlacementState _placeState;
    public PlacementState PlaceState
    {
        get { return _placeState; }
        set 
        { 
            _placeState = value; 
            onPreparationStateChange?.Invoke(value);
        }
    }

    public PlacementDetail[] placementDetail;
    public TowerDetail[] towers;

    public static UnityAction<PlacementState> onPreparationStateChange;
    public static UnityAction onTowerPlaced;
    public static UnityAction onTowerDeleted;

    [SerializeField] CinemachineVirtualCamera preparationVcam;
    [SerializeField] PreparataioUIManager uiManager;

    bool canPlace = false;
    int towerIdx = 0;
    float nextStageCooldownCounter;

    private void Awake()
    {
        GameplayManager.instance.onStateChange += CheckState;
    }

    private void OnDestroy()
    {
        GameplayManager.instance.onStateChange -= CheckState;
    }

    private void Update()
    {
        if (GameplayManager.instance.State == GameplayState.Night)
            return;

        nextStageCooldownCounter += Time.deltaTime;

        if(nextStageCooldownCounter > 300)
        {
            nextStageCooldownCounter = 0;
            GameplayManager.instance.ChangeToNight();
        }

        uiManager.SetCoolDownText(nextStageCooldownCounter);
    }

    bool CheckTowerPlacementPermit()
    {
        float sumResources = GameplayManager.instance.Resources - towers[towerIdx].costToUse; 
        
        if(sumResources >= 0)
            GameplayManager.instance.Resources = sumResources;
        
        return sumResources >= 0 && canPlace;
    }

    public void PlaceTower(int placementIdx)
    {
        switch (PlaceState)
        {
            case PlacementState.Place:
                if (placementDetail[placementIdx].currentTower == null && CheckTowerPlacementPermit())
                {
                    placementDetail[placementIdx].currentTower = Instantiate(towers[towerIdx].towerPrefab, placementDetail[placementIdx].placement);
                    onTowerPlaced?.Invoke();
                    canPlace = false;
                }
                break;
            case PlacementState.Delete:
                if (placementDetail[placementIdx].currentTower != null)
                {
                    Destroy(placementDetail[placementIdx].currentTower);
                    placementDetail[placementIdx].currentTower = null;
                    onTowerDeleted?.Invoke();
                    PlaceState = PlacementState.Place;
                }
                break;
            default:
                break;
        }
    }

    public void SelectCurrentTower(int index)
    {
        towerIdx = index;
        canPlace = true;
    }

    public void UnSetCanPlace() => canPlace = false;

    public void SetDeleteCondition()
    {
        if(PlaceState == PlacementState.Delete)
            PlaceState = PlacementState.Place;
        else
            PlaceState = PlacementState.Delete;
    }

    void CheckState(GameplayState state)
    {
        switch (state)
        {
            case GameplayState.Day:
                preparationVcam.Priority = 100;
                uiManager.InitiateTowerButtons(towers.Length, towers,SelectCurrentTower);
                break;
            case GameplayState.Night:
                preparationVcam.Priority = 0;
                break;
            default:
                break;
        }
    }
}
