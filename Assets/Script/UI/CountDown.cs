using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour {

    private Animator animator;
    private bool bFinish;
    public bool IsFinish
    {
        get { return bFinish; }
    }

    public bool bLetsBonvoyage;
    public bool LetsBonyage
    {
        get { return bLetsBonvoyage; }
    }

    public int seCD;
    private int preCD;
    public int seBonvoyage;
    private int preBonvoyage;

    public bool bPlayCD;
    public bool bPlayBonvyage;

    private AudioSource audioSource;
    public AudioClip audioCD;
    public AudioClip audioBonvoyage;

	// Use this for initialization
	void Start () {
		
        animator = GetComponent<Animator>();
        bFinish = false;

        bLetsBonvoyage = false;

        seCD = 0;
        preCD = seCD;
        seBonvoyage = 0;
        preBonvoyage = seBonvoyage;


        bPlayCD = false;
        bPlayBonvyage = false;

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
//        PlayCountDownSE();        
//        PlayBonvoyageSE();
	}


    public void StartCountDown()
    {
        bLetsBonvoyage = false;
        bPlayCD = false;
        bPlayBonvyage = false;
        bFinish = false;

        animator.SetTrigger("CountDownStart");
//        PlayCountDownSE(true);

        StartCoroutine(IsFinishCountDown());
    }

    private IEnumerator IsFinishCountDown()
    {
        while( true )
        {
            AnimatorStateInfo nowState = animator.GetCurrentAnimatorStateInfo(0);
            if (nowState.IsName("ReadyGoFinish"))
            {
                bFinish = true;
                break;
            }
            yield return null;
        }    
    }

    public void PlayCountDownSE()
    {
        audioSource.PlayOneShot(audioCD, 1.0f);
        /*
        if( bPlayCD )
        {
            audioSource.PlayOneShot(audioCD, 1.0f);
            
        }
        bPlayCD = false;
        */
    }
    
    public void PlayBonvoyageSE()
    {        
        audioSource.PlayOneShot(audioBonvoyage, 1.0f);
        /*
        if( bPlayBonvyage )
        {
            audioSource.PlayOneShot(audioBonvoyage, 1.0f);
        }
        bPlayBonvyage = false;
        */

    }

}
