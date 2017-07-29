using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Idle,
    Event,
    Loading
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }

    // Editor values
    public int gameSpeed;
    public Transform canvasTransform;
    public Battery battery;
    public CycleCounter cycleCounter;
    public ResourceContainer resourceContainer;
    public Light dLight;

    private void Awake () {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        State = GameState.Loading;

        // Initial game speed
        gameSpeed = 1;
    }
	
	private void Update () {
        // TODO: Exit handling?
    }

    private void LateUpdate()
    {
        if (!Idle)
            return;

        CheckKeyInput();
    }

    private void CheckKeyInput()
    {
        // Key input if necessary?
    }

    public void SetState(GameState state)
    {
        State = state;
    }

    public void End()
    {
        State = GameState.Event;
        dLight.color = Color.black;
    }

    public bool Idle
    {
        get { return State == GameState.Idle; }
    }
}
