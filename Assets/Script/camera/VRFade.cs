using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class VRFade : MonoBehaviour {

    private Canvas fadeCanvas;
    private Image fadeImage;

    private int counter;
    private int FadeTime;
    private float fadeBlend;

    public enum VRFadeType
    {
        VRFADE_NONE = 0,
        VRFADE_IN,
        VRFADE_OUT,
        VRFADE_MAX,
    };
    private VRFadeType fadeType;

    public bool IsFade()
    {
        return fadeType != VRFadeType.VRFADE_NONE;
    }

    void Awake()
    {
        fadeCanvas = gameObject.GetComponentInChildren<Canvas>();
        if( fadeCanvas != null )
        {
            fadeImage = fadeCanvas.GetComponentInChildren<Image>();
        }
    }

	// Use this for initialization
	void Start () {
        counter = 0;
        FadeTime = 0;
        fadeType = VRFadeType.VRFADE_NONE;

        Color col = fadeImage.color;
        col.a = 0.0f;
        fadeImage.color = col;
	}
	
	// Update is called once per frame
	void Update () {
		
        bool bFinish = false;
        switch( fadeType )
        {
        case VRFadeType.VRFADE_IN:
            bFinish = FadeIn();
            break;
        case VRFadeType.VRFADE_OUT:
            bFinish = FadeOut();
            break;
        }

        // フェードカウンター
        if( IsFade() )
        {
            counter++;
            if( counter > FadeTime )
            {
                counter = FadeTime;
                if( bFinish )
                {
                    Debug.Log("Fade" + fadeType + "Is Finish");
                    fadeType = VRFadeType.VRFADE_NONE;
                    counter = 0;
                }
            }
        }	
	}

    public void StartFade(VRFadeType type, int fadeTime)
    {
        fadeType = type;
        FadeTime = fadeTime;
        counter = 0;

        // 現在のα値に合わせて開始時間を設定
        Color col = fadeImage.color;
        if( type == VRFadeType.VRFADE_IN )
        {
            col.a = 1.0f;
            fadeImage.color = col;
        }
        else if( type == VRFadeType.VRFADE_OUT )
        {
            col.a = 0.0f;
            fadeImage.color = col;
        }        
    }

    private bool FadeIn()
    {
        Color fadeColor = fadeImage.color;

        fadeBlend = Mathf.Min((float)(counter) / (float)(FadeTime), 1.0f);
        float alpha = Mathf.Lerp(1.0f, 0.0f, fadeBlend);

        fadeColor.a = alpha;

        fadeImage.color = fadeColor;

        if( alpha <= 0.0f )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool FadeOut()
    {
        Color fadeColor = fadeImage.color;

        fadeBlend = Mathf.Min((float)(counter) / (float)(FadeTime), 1.0f);
        float alpha = Mathf.Lerp(0.0f, 1.0f, fadeBlend);

        fadeColor.a = alpha;

        fadeImage.color = fadeColor;

        if( alpha >= 1.0f )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
