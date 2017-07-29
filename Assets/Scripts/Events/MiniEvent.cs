using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniEvent : MonoBehaviour
{
    public bool Complete { get; protected set; }
    public MiniEvent previousEvent;
    public float delay;
    
    protected virtual void CompleteEvent()
    {
        Complete = true;
        Destroy(gameObject);
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
            if (previousEvent || Complete || LoadingScreen.Instance.IsLoading)
                return false;
            return true;
        }
    }
}
