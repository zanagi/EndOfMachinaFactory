﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VNManager : MonoBehaviour {

    public static VNManager Instance { get; private set; }

    [SerializeField]
    private VNImagePanel imagePanel;
    [SerializeField]
    private VNTextPanel textPanel;
    [SerializeField]
    private GameObject blockPanel;

    private Type currentActionType;

	// Use this for initialization
	void Start () {
		if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        textPanel.gameObject.SetActive(false);
	}
	
	public void SetText(TextPanelType type, string text, string characterName)
    {
        textPanel.SetTargetTextAndName(type, text, characterName);
    }
    
    public void SetText(VNTextAction action)
    {
        SetText(action.TpType, action.Text, action.Name);
    }

    public void SetImage(VNImageAction action)
    {
        imagePanel.SetImage(action.ImageName, action.Position);
    }

    public void SetAction(VNAction action)
    {
        currentActionType = action.GetType();
        if (currentActionType == typeof(VNTextAction))
        {
            SetText(action as VNTextAction);
        } else if(currentActionType == typeof(VNImageAction))
        {
            SetImage(action as VNImageAction);
        }
        blockPanel.SetActive(true);
    }

    public void CompleteCurrentText()
    {
        textPanel.CompleteText();
    }

    public void Hide()
    {
        textPanel.Hide();
        blockPanel.SetActive(false);
    }

    public bool ActionComplete
    {
        get { return (CurrentActionIsText && textPanel.Complete) || (CurrentActionIsImage && imagePanel.Complete); }
    }

    public bool CurrentActionIsText
    {
        get { return currentActionType == typeof(VNTextAction); }
    }

    public bool CurrentActionIsImage
    {
        get { return currentActionType == typeof(VNImageAction); }
    }

    public bool TextPanelActive
    {
        get { return textPanel.gameObject.activeSelf; }
    }

    public bool Animating
    {
        get { return textPanel.Animating; }
    }
}
