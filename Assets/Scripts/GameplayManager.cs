using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameplayState
{
    Day, Night
}

[DefaultExecutionOrder(-10)]
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    private GameplayState _state;
    public GameplayState State
    {
        get { return _state; }
        set 
        {
            if(value != _state)
                _state = value;
            Debug.Log("Change to " + _state);

            onStateChange?.Invoke(_state);
        }
    }
    private float _resources;
    public float Resources
    {
        get { return _resources; }
        set 
        { 
            _resources = value;
            onResourcesChange?.Invoke(_resources);
        }
    }

    public int waveIndex = -1;
    public GameObject PreparationUI;
    public GameObject BattleUI;
    public Camera Camera;

    public UnityAction<GameplayState> onStateChange;
    public UnityAction<float> onResourcesChange;
    public UnityAction<int> onWaveChange;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        ChangeToDay();
        Resources = 150;
    }

    [ContextMenu("Change To Night")]
    public void ChangeToNight()
    {
        State = GameplayState.Night;
        Camera.backgroundColor = Color.black;
        PreparationUI.SetActive(false);
        BattleUI.SetActive(true);
        onResourcesChange(Resources);
        onWaveChange?.Invoke(waveIndex);
    }

    [ContextMenu("Change To Day")]
    public void ChangeToDay()
    {
        waveIndex++;
        State = GameplayState.Day;
        Camera.backgroundColor = Color.white;
        PreparationUI.SetActive(true);
        BattleUI.SetActive(false);
        onResourcesChange(Resources);
        onWaveChange?.Invoke(waveIndex);
    }

    public void Lost()
    {
        SceneManager.LoadSceneAsync("Lose Scene");
    }

    public void Win()
    {
       SceneManager.LoadSceneAsync("Win Scene");
    }
}
