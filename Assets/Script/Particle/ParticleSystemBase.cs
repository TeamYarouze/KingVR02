using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemBase : MonoBehaviour {

    private ParticleSystem psc = null;
    private bool bPlay;

    void Awake()
    {
        psc = GetComponent<ParticleSystem>();
        bPlay = false;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
        if( !psc ) return;

        if( bPlay )
        {
            if( psc.isStopped )
            {
                Destroy(gameObject);
            }
        }
        	
	}

    public void Play()
    {
        if( !psc ) return;

        psc.Play();
        bPlay = true;
    }

    public void Stop()
    {
        if( !psc ) return;

        psc.Stop();
        bPlay = false;
    }

    public bool IsPlay()
    {
        if( !psc ) return false;

        return psc.isPlaying;
    }

    public bool IsLoop()
    {
        if( !psc ) return false;

        return psc.main.loop;
    }
}
