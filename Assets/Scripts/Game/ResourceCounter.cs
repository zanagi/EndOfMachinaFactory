using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Resource
{
    X, Y, Z, XYZ
}

public class ResourceCounter : MonoBehaviour {

    private static readonly int maxResourceAmount = 99;

    [SerializeField]
    private Resource resource;
    [SerializeField]
    private Text numberText;
    [SerializeField]
    private int resourceCount = 50; // Initial resource count set in editor

    private void Start()
    {
        UpdateText();
    }
    
    private void UpdateText ()
    {
        numberText.text = resourceCount.ToString();
    }

    public bool ChangeResourceCount(int delta)
    {
        int newCount = resourceCount + delta;

        if(newCount >= 0)
        {
            resourceCount = Mathf.Min(newCount, maxResourceAmount);
            UpdateText();
            return true;
        }
        return false;
    }
}
