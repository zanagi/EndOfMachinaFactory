using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMSwitcher : MonoBehaviour {

    private AudioSource bgm;

	// Use this for initialization
	void Start () {
        bgm = GetComponent<AudioSource>();

        AudioManager.Instance.PlayBGM(bgm);
	}
}
