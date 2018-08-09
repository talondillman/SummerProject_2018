using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroAdder : MonoBehaviour {

    public AudioSource musicSource;
    public AudioClip musicStart;

	// Use this for initialization
	void Start () {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
