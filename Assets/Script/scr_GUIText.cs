using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
#if UNITY_PS4
using UnityEngine.PS4;
#endif  //

public class scr_GUIText : MonoBehaviour {

    private GameObject textObj = null;
    private bool bDraw = false;
    private bool bGUIDraw = false;

    // fps計測
    private float fps;
    private int updateCount;
    private float timeElapsed;

#if UNITY_PS4
    private Utility.VideoOutResolutionStatus voInfo;
#endif  //

    // シングルトン
    private static scr_GUIText _instance;
    public static scr_GUIText instance
    {
        get
        {
            if( _instance == null )
            {
                _instance = FindObjectOfType<scr_GUIText>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if( _instance == null )
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else if( this != _instance )
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        fps = 0.0f;
        updateCount = 0;
        timeElapsed = 0.0f;
    }

	// Use this for initialization
	void Start () {
		textObj = GameObject.Find("Text");
        bDraw = false;
        bGUIDraw = false;

 	}
	
	// Update is called once per frame
	void Update () {
//        CalcFPS();

        if( !textObj ) return;

        if( VRSettings.enabled )
        {
            bDraw = false;
        }
        else
        {
            if( Input.GetButtonDown("L1") )
            {
                bDraw = !bDraw;
                bGUIDraw = !bGUIDraw;
            }
        }
        /*
        // VRモード時は描画しない
        if( VRSettings.enabled )
        {
            bDraw = false;
        }
        */       

        textObj.GetComponent<GUIText>().text = "";
	}


    void LateUpdate()
    {
        CalcFPS();
    }
    
    public void SetText(string str)
    {
        if( !textObj ) return;
        if( !bDraw ) return;

        textObj.GetComponent<GUIText>().text = str;
    }

    public void AddText(string str)
    {
        if( !textObj ) return;
        if( !bDraw ) return;

        textObj.GetComponent<GUIText>().text += str;
    }

    public void SetDrawFlag(bool flg)
    {
        bDraw = flg;
    }

    void OnGUI()
    {
        if( !bGUIDraw ) return;

        GUI.Label(new Rect(1, 1, 300, 100), "FPS:"+fps+"\nTargetFPS: " + Application.targetFrameRate );

#if UNITY_PS4
        GUI.Label(new Rect(1, 110, 300, 100), "RefleshRate : " + voInfo.refreshRate + "\n" +
                                              "DeltaTime : "+Time.deltaTime + "\n" + 
                                              "FixedDeltaTime : " + Time.fixedDeltaTime + "\n");
#endif  //
    }

    private void CalcFPS()
    {
        updateCount++;
        timeElapsed += Time.deltaTime;
        if( timeElapsed >= 1.0f )
        {
            fps = (float)(updateCount) / timeElapsed;
            updateCount = 0;
            timeElapsed = 0.0f;
        }
    }
}