using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }

    private AsyncOperation loadOperation;
    private string targetSceneName;

    private void Start()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (loadOperation == null)
            return;

        if (loadOperation.isDone)
        {
            loadOperation = null;
            LoadingScreen.Instance.StartOutAnimation();
            LoadingScreen.Instance.CompleteHandler += EndLoadingState;
        }
    }

    public void LoadScene(string targetSceneName, LoadingStyle loadingStyle = LoadingStyle.Fade)
    {
        this.targetSceneName = targetSceneName;
        LoadingScreen.Instance.StartInAnimation(loadingStyle);
        LoadingScreen.Instance.CompleteHandler += BeginLoadingScene;

        // Set loading state
        GameManager.Instance.SetState(GameState.Loading);
    }

    private void BeginLoadingScene()
    {
        loadOperation = SceneManager.LoadSceneAsync(targetSceneName);
        LoadingScreen.Instance.CompleteHandler -= BeginLoadingScene;
    }

    private void EndLoadingState()
    {
        LoadingScreen.Instance.Hide();
        LoadingScreen.Instance.CompleteHandler -= EndLoadingState;

        // Set idle state
        GameManager.Instance.SetState(GameState.Idle);
    }

    public string ActiveSceneName
    {
        get { return SceneManager.GetActiveScene().name; }
    }
} 
