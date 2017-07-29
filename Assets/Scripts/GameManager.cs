﻿using UnityEngine;
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
    }
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && !LoadingScreen.Instance.IsLoading)
        {
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
