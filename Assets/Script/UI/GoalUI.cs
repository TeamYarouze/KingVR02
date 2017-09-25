using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUI : MonoBehaviour {

    public AudioClip audioClip;
    private AudioSource audioSource;

    private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGoalUI()
    {
        animator.SetTrigger("IsGoalTrigger");
    }

    public void StopGoalUI()
    {
        animator.SetTrigger("GoIdle");
    }

    public void PlayGoalJingle()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
