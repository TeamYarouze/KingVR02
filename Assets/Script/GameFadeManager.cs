using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;


public class GameFadeManager : Singleton<GameFadeManager> {

    private Canvas fadeCanvas;
    private Image fadeImage;

    private VRFade vrFade = null;

    private float counter;
    private int FadeTime;
    private float fadeBlend;

    public enum FadeType
    {
        FADE_NONE = 0,
        FADE_IN,
        FADE_OUT,
        FADE_MAX,
    };
    private FadeType fadeType;

    public bool IsFade()
    {
        if (vrFade != null)
        {
            return vrFade.IsFade();
        }
        else
        {
            return fadeType != FadeType.FADE_NONE;
        }
    }

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        fadeCanvas = gameObject.GetComponentInChildren<Canvas>();

        if( fadeCanvas != null )
        {
            fadeImage = fadeCanvas.GetComponentInChildren<Image>();
        }
    }

	// Use this for initialization
	void Start () {
		
        counter = 0.0f;
        FadeTime = 0;
        fadeType = FadeType.FADE_NONE;

        Color col = fadeImage.color;
        col.a = 0.0f;
        fadeImage.color = col;
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if( vrFade != null )
        {
            return ;
        }
        */

        bool bFinish = false;
        switch( fadeType )
        {
        case FadeType.FADE_IN:
            bFinish = FadeIn();
            break;
        case FadeType.FADE_OUT:
            bFinish = FadeOut();
            break;
        }

        // フェードカウンター
        if( IsFade() )
        {
            counter += GameDefine.FPSDeltaScale();

            if( (int)counter > FadeTime )
            {
                counter = FadeTime;
                if( bFinish )
                {
                    Debug.Log("Fade" + fadeType + "Is Finish");
                    fadeType = FadeType.FADE_NONE;
                    counter = 0.0f;
                }
            }
        }
	}

    public void StartFade(FadeType type, int fadeTime)
    {
        if( vrFade != null )
        {
            vrFade.StartFade((VRFade.VRFadeType)(type), fadeTime);
            _StartFade(type, fadeTime);
        }
        else
        {
            _StartFade(type, fadeTime);
        }
    }

    private void _StartFade(FadeType type, int fadeTime)
    {
        fadeType = type;
        FadeTime = fadeTime;
        counter = 0;

        // 現在のα値に合わせて開始時間を設定
        Color col = fadeImage.color;
        if( type == FadeType.FADE_IN )
        {
            col.a = 1.0f;
            fadeImage.color = col;
        }
        else if( type == FadeType.FADE_OUT )
        {
            col.a = 0.0f;
            fadeImage.color = col;
        }        
    }

    private bool FadeIn()
    {
        Color fadeColor = fadeImage.color;

        fadeBlend = Mathf.Min(counter / (float)(FadeTime), 1.0f);
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

        fadeBlend = Mathf.Min(counter / (float)(FadeTime), 1.0f);
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

    /**
     *  親子関係の設定
     *  @param[in] parent 親OBJ (nullで親子関係を解消)
     */
    public void SetParent(GameObject parent)
    {
        if( parent != null )
        {
            transform.parent = parent.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    /**
     * VRモード時の設定
     */
    public void SetupVRMode(string cameraName)
    {
        if( !VRSettings.enabled )
        {
            ResetVRMode();
            return;
        }

        if( vrFade != null )
        {
            vrFade = null;
        }

        GameObject cam = GameObject.Find(cameraName);
        if( cam )
        {
            vrFade = cam.GetComponent<VRFade>();
        }
    }
    /**
     * VRモードの解除
     */
    public void ResetVRMode()
    {
        if( vrFade != null )
        {
            vrFade = null;
        }
    }
}
