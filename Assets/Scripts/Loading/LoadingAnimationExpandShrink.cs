using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimationExpandShrink : LoadingAnimation
{
    private Vector2 minRect, maxRect, delta;

    public LoadingAnimationExpandShrink(Image image, Vector2 minRect, Vector2 maxRect, Vector2 delta)
        : base(image)
    {
        this.minRect = minRect;
        this.maxRect = maxRect;
        this.delta = delta;
    }
    
    public override bool UpdateAnimation(LoadingMode mode)
    {
        if (mode == LoadingMode.Idle)
        {
            return false;
        }
        else if (mode == LoadingMode.In)
        {
            image.rectTransform.sizeDelta += delta;
            if(image.rectTransform.sizeDelta.x >= maxRect.x && image.rectTransform.sizeDelta.y >= maxRect.y)
            {
                image.rectTransform.sizeDelta = maxRect;
                return true;
            }
            return false;
        }
        image.rectTransform.sizeDelta -= delta;

        if(image.rectTransform.sizeDelta.x <= minRect.x && image.rectTransform.sizeDelta.y <= minRect.y)
        {
            image.rectTransform.sizeDelta = minRect;
            return true;
        }
        return false;
    }

    public override void FastForward()
    {
        image.rectTransform.sizeDelta = maxRect;
    }
}

