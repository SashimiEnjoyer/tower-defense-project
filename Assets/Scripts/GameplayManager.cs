using UnityEngine;
using UnityEngine.Events;

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

    public float Resources;
    public GameObject PreparationUI;

    public UnityAction<GameplayState> onStateChange;

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
        Resources = 10;
    }

    public void ChangeState(GameplayState state) => State = state;

    [ContextMenu("Change To Night")]
    public void ChangeToNight()
    {
        State = GameplayState.Night;
        PreparationUI.SetActive(false);
    }

    [ContextMenu("Change To Day")]
    public void ChangeToDay()
    {
        State = GameplayState.Day;
        PreparationUI.SetActive(true);
    }
}
