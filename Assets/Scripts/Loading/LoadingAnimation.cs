using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LoadingAnimation
{
    public Image image { get; private set; }

    public LoadingAnimation(Image image)
    {
        this.image = image;
    }

    public abstract void FastForward();

    public abstract bool UpdateAnimation(LoadingMode mode);
}

