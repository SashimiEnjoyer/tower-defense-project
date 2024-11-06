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
    public GameObject PreparationUI;
    public GameObject BattleUI;
    public Camera Camera;

    public UnityAction<GameplayState> onStateChange;
    public UnityAction<float> onResourcesChange;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        ChangeState(GameplayState.Day);
        Resources = 50;
    }

    public void ChangeState(GameplayState state) => State = state;

    [ContextMenu("Change To Night")]
    public void ChangeToNight()
    {
        State = GameplayState.Night;
        Camera.backgroundColor = Color.black;
        PreparationUI.SetActive(false);
        BattleUI.SetActive(true);
    }

    [ContextMenu("Change To Day")]
    public void ChangeToDay()
    {
        State = GameplayState.Day;
        Camera.backgroundColor = Color.white;
        PreparationUI.SetActive(true);
        BattleUI.SetActive(false);
    }

    public void Lost()
    {
        SceneManager.LoadSceneAsync("Lose Scene");
    }
}
