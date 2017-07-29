using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private List<AudioSource> audioSources;

    // BGM / Mute
    private AudioSource bgm, nextBgm;
    private float volumeFadeSpeed = 0.01f;
    private bool bgmFade, test;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioSources = new List<AudioSource>();
        audioSources.AddRange(GetComponentsInChildren<AudioSource>());
    }

    private void Update()
    {
        // BGM fade by lowering the volume
        if (bgm && bgmFade)
        {
            bgm.volume -= volumeFadeSpeed;

            if (bgm.volume < volumeFadeSpeed)
            {
                bgm.Stop();
                bgm.volume = 1.0f;
                bgm = null;
                bgmFade = false;
                SwapBGM();
            }
        }
    }

    private void SwapBGM()
    {
        if(nextBgm)
        {
            bgm = nextBgm;
            bgm.Play();
            nextBgm = null;
        }
    }

    public void AddAudioSources(AudioSource[] sources)
    {
        audioSources.AddRange(sources);
    }

    public void RemoveAudioSources(AudioSource[] sources)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            audioSources.Remove(sources[i]);
            if (sources[i] == bgm)
            {
                bgmFade = true;
                continue;
            }
            sources[i].Stop();
        }
    }

    public void PlayAudio(string name, bool isBgm = false)
    {
        for(int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].clip.name == name || audioSources[i].name == name)
            {
                if (isBgm)
                {
                    if (bgm)
                    {
                        nextBgm = audioSources[i];
                        bgmFade = true;
                        return;
                    }
                    bgm = audioSources[i];
                }
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlayBGM(AudioSource newBgm)
    {
        nextBgm = newBgm;
        if (!bgm)
        {
            SwapBGM();
            return;
        }
        bgmFade = true;
    }

    public void StopAudio(string name)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].clip.name == name || audioSources[i].name == name)
                audioSources[i].Stop();
        }
    }
}

