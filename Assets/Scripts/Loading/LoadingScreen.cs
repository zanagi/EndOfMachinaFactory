using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum LoadingMode
{
    Idle, In, Out
}

public enum LoadingStyle
{
    Fade, ExpandAndShrink
}

public class LoadingScreen : MonoBehaviour {

    public static LoadingScreen Instance { get; private set; }
    
    public Image fadeAnimationImage, expandShrinkAnimationImage;
    public float startFadeSpeed = 0.01f;

    // On complete event handler
    public delegate void OnComplete();
    public event OnComplete CompleteHandler;

    // Is loading property
    public bool IsLoading { get { return loadingMode != LoadingMode.Idle; } }

    private LoadingAnimation currentAnimation, fadeAnimation, expandShrinkAnimation;
    private LoadingMode loadingMode;

    private void Start() {
		if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        loadingMode = LoadingMode.Idle;

        // Initialize animations
        fadeAnimation = new LoadingAnimationFade(fadeAnimationImage, 0.02f);
        expandShrinkAnimation = 
            new LoadingAnimationExpandShrink(expandShrinkAnimationImage, Vector2.zero, new Vector2(1056, 1254), new Vector2(16, 19));

        if(startFadeSpeed > 0)
        {
            currentAnimation = new LoadingAnimationFade(fadeAnimationImage, startFadeSpeed);
            currentAnimation.FastForward();
            StartOutAnimation();
            CompleteHandler += Hide;
        }  else
        {
            GameManager.Instance.SetState(GameState.Idle);
        }
	}

    private void FixedUpdate()
    {
        if (loadingMode == LoadingMode.Idle)
            return;

        if(currentAnimation.UpdateAnimation(loadingMode))
        {
            loadingMode = LoadingMode.Idle;

            if (CompleteHandler != null)
                CompleteHandler();
        }
    }

    public void StartInAnimation(LoadingStyle style)
    {
        if (style == LoadingStyle.Fade)
            currentAnimation = fadeAnimation;
        else if (style == LoadingStyle.ExpandAndShrink)
            currentAnimation = expandShrinkAnimation;
        else
            currentAnimation = fadeAnimation; // Default to fade animation

        // Assing loading mode
        loadingMode = LoadingMode.In;

        // Show mask
        Show();
    }

    public void StartOutAnimation()
    {
        loadingMode = LoadingMode.Out;
        Show();
    }

    public void Show()
    {
        currentAnimation.image.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if(startFadeSpeed > 0)
        {
            startFadeSpeed = -1.0f;
            CompleteHandler -= Hide;
            GameManager.Instance.SetState(GameState.Idle);
        }
        currentAnimation.image.gameObject.SetActive(false);
    }
}
