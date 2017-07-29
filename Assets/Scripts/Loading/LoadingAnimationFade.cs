using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimationFade : LoadingAnimation
{
    private float fadeSpeed = 0.02f;

    public LoadingAnimationFade(Image image, float fadeSpeed)
        : base(image)
    {
        this.fadeSpeed = fadeSpeed;
    }
    
    public override bool UpdateAnimation(LoadingMode mode)
    {
        if(mode == LoadingMode.Idle)
        {
            return false;
        } else if(mode == LoadingMode.In)
        {
            SetImageOpacity(image.color.a + fadeSpeed);

            if (image.color.a >= 0.999f)
            {
                SetImageOpacity(1.0f);
                return true;
            }
            return false;
        }
        SetImageOpacity(image.color.a - fadeSpeed);

        if(image.color.a <= 0.001f)
        {
            SetImageOpacity(0.0f);
            return true;
        }
        return false;
    }

    public override void FastForward()
    {
        SetImageOpacity(1.0f);
    }

    private void SetImageOpacity(float opacity)
    {
        Color temp = image.color;
        temp.a = opacity;
        image.color = temp;
    }
}

