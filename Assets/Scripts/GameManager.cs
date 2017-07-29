using UnityEngine;

public enum GameState
{
    Idle,
    Game,
    Event,
    Loading
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; }
    
	// Use this for initialization
	void Awake () {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Values
        State = GameState.Loading;
	}
	
	// Update is called once per frame
	void Update () {
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
    
    public void SetState(GameState state)
    {
        State = state;
    }
}
