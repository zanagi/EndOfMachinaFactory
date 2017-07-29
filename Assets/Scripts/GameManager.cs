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
    public int gameSpeed;
    
    [SerializeField]
    private Transform cameraTarget, cameraBounds;
    private Vector3 cameraMin, cameraMax;

    [SerializeField]
    private float cameraSpeed = 0.1f;
    
    // Use this for initialization
    private void Awake () {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Values
        State = GameState.Loading;

        // Initial game speed
        gameSpeed = 1;

        // Camera bounds
        cameraMin = new Vector3(-0.5f * cameraBounds.localScale.x - cameraBounds.localScale.z, 0, -0.5f * cameraBounds.localScale.z);
        cameraMax = new Vector3(0.5f * cameraBounds.localScale.x + cameraBounds.localScale.z, 0, -0.5f * cameraBounds.localScale.z);
        
        Debug.Log("Min: " + cameraMin);
        Debug.Log("Max: " + cameraMax);
        Debug.Log("Up: " + Camera.main.transform.up);
        Debug.Log("Right: " + Camera.main.transform.right);
    }
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && !LoadingScreen.Instance.IsLoading)
        {
            // TODO: Replace with proper exit logic
            if(LoadingManager.Instance.ActiveSceneName != Scenes.Intro)
            {
                LoadingManager.Instance.LoadScene(Scenes.Intro, LoadingStyle.ExpandAndShrink);
                return;
            }
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        if (State != GameState.Idle)
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
}
