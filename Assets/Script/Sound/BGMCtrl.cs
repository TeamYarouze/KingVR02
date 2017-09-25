using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCtrl : MonoBehaviour {

    public AudioClip bgm;
    public AudioClip jingle;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBGM()
    {
        StopBGM();

        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlayJingle()
    {
        StopBGM();

        audioSource.clip = jingle;
        audioSource.loop = false;
        audioSource.Play();
    }
}
