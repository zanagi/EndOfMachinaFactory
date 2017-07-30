using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniEvent : MonoBehaviour
{
    public bool active = true;
    public bool Complete { get; protected set; }

    [SerializeField]
    protected MiniEvent nextEvent;
    [SerializeField]
    protected float delay;

    protected virtual void CompleteEvent()
    {
        Complete = true;
        Destroy(gameObject);

        if(nextEvent)
        {
            nextEvent.active = true;
        } else
        {
            GameManager.Instance.SetState(GameState.Idle);
        }
    }

    protected virtual void LateUpdate()
    {
        // Fast-forward event in editor
        if(Application.isEditor && Input.GetKeyUp(KeyCode.Space))
        {
            CompleteEvent();
        }
    }

    protected bool CanUpdate
    {
        get
        {
            if (!active || Complete || LoadingScreen.Instance.IsLoading)
                return false;
            return true;
        }
    }
}
