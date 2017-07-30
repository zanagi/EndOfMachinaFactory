using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Collections.Generic;


// Displays text in a visual novel like manner
public class VNTextEvent : MiniEvent
{
    public TextAsset textAsset;

    private List<VNAction> actions;
    private int unitIndex;
    private bool actionActive, hideStarted;

    private static StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
    private static readonly string commentPrefix = "#", imagePrefix = "*";

    private void Start()
    {
        // Split text to lines
        var lines = textAsset.text.Split(new[] { '\r', '\n' }, options);

        // Map each line to a text unit
        actions = new List<VNAction>();

        foreach(var line in lines)
        {
            var action = TextToAction(line);

            if (action != null)
                actions.Add(action);
        }
    }

    private VNAction TextToAction(string text)
    {
        string trim = text.Trim();

        if(trim.Length == 0 || trim.StartsWith(commentPrefix))
        {
            return null;
        }
        else if(trim.StartsWith(imagePrefix))
        {
            return new VNImageAction(trim.Substring(imagePrefix.Length));
        }
        return new VNTextAction(trim);
    }

    private void Update()
    {
        if (!CanUpdate || actionActive)
            return;

        if(GameManager.Instance.Idle)
            GameManager.Instance.SetState(GameState.Event);

        if (actions.Count == unitIndex)
        {
            if(hideStarted)
            {
                if (!VNManager.Instance.TextPanelActive)
                    CompleteEvent();
                return;
            }
            VNManager.Instance.Hide();
            hideStarted = true;;
            return;
        }
        VNManager.Instance.SetAction(actions[unitIndex]);
        actionActive = true;
    }

    protected override void LateUpdate()
    {
        if (!actionActive || VNManager.Instance.Animating)
            return;
        
        // Image event
        if (VNManager.Instance.CurrentActionIsImage && VNManager.Instance.ActionComplete)
        {
            actionActive = false;
            unitIndex++;
            return;
        }

        if (VNManager.Instance.CurrentActionIsText)
        {
            // Text event
            if (InputHandler.Instance.Clicked)
            {
                if (VNManager.Instance.ActionComplete)
                {
                    actionActive = false;
                    unitIndex++;
                    return;
                }
                VNManager.Instance.CompleteCurrentText();
            }
        }
        base.LateUpdate();
    }
}
