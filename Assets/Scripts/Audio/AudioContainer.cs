using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    public AudioSource[] sources { get; private set; }

    private void Start()
    {
        sources = GetComponentsInChildren<AudioSource>();
        AudioManager.Instance.AddAudioSources(sources);
    }

    private void OnDestroy()
    {
        AudioManager.Instance.RemoveAudioSources(sources);
    }
}
