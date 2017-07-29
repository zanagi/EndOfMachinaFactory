using System;
using UnityEngine;


public class VNAction
{
    public bool Complete { get; set; }

    protected static readonly char actionSplit = ';';
}

public class VNTextAction : VNAction
{
    public string Name { get; private set; }
    public string Text { get; private set; }
    public TextPanelType TpType { get; private set; }
    
    public VNTextAction(string vnLine)
    {
        Parse(vnLine.Split(actionSplit));
    }

    private void Parse(string[] split)
    {
        if (split.Length == 1)
        {
            Name = string.Empty;
            Text = split[0].Trim();
            TpType = TextPanelType.Narration;
        }
        else if (split.Length == 2)
        {
            Name = split[0].Trim();
            Text = split[1].Trim();
            TpType = TextPanelType.Normal;
        }
        else if (split.Length == 3)
        {
            Name = split[0].Trim();
            Text = split[1].Trim();

            var tptStr = split[2].Trim();
            if (tptStr == "narration")
                TpType = TextPanelType.Narration;
            else if (tptStr == "thinking")
                TpType = TextPanelType.Thinking;
            else
                TpType = TextPanelType.Normal;
        }
    }
}

public class VNImageAction : VNAction
{
    public string ImageName { get; private set; }
    public ImagePanelPosition Position { get; private set; }

    public VNImageAction(string vnLine)
    {
        Parse(vnLine.Split(actionSplit));
    }

    private void Parse(string[] split)
    {
        if(split.Length == 0)
        {
            Complete = true;
            return;
        }
        ImageName = split[0].Trim();

        if(split.Length > 1)
        {
            var trim = split[1].Trim();
            if (trim == "all")
                Position = ImagePanelPosition.All;
            else if (trim == "right")
                Position = ImagePanelPosition.Right;
        }
    }
}