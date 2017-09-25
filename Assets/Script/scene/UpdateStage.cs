using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.VR;

public class UpdateStage : UpdateBase {

    private GameObject DustStorm;
    public ParticleSystemBase particleBase
    {
        get { return (DustStorm) ? DustStorm.GetComponent<ParticleSystemBase>() : null; }
    }

    private GameObject bgmObject;
    private bool m_bPlayJingleOneShot;

    private int m_stage;            // ステージ数
    public int Stage
    {
        set { m_stage = value; }
        get { return m_stage;  }
    }

    private GameObject playerObj;
    private GameObject socicalCamRoot;

    // ステージステート
    public enum StageState
    {
        STATE_PREPARE,
        STATE_READY,
        STATE_INGAME,
        STATE_GOAL,
        STATE_FAILE,
        STATE_GAMEOVER,
        STATE_RESET,

        STATE_NUM,
    };
    private StageState m_state;
    public StageState CurrentState
    {
        get { return m_state; }
    }
    private int m_counterSec;                  // 汎用カウンター
    private float m_frame;

    private int m_subStep;

    new void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        cameraMngr = new CameraManager();
        cameraMngr.Initialize();
        GameFadeManager.Instance.SetupVRMode(Camera.main.name);

        if( VRSettings.enabled )
        {
            VRManager.Instance.ToggleHMDViewOnMonitor(false);
//            cameraMngr.EnableSocialCamera();
        }

        m_stage = GameModeData.StageCount;      // ステージ数を設定
        m_counterSec = 0;
        m_frame = 0.0f;
    }

	// Use this for initialization
	new void Start () {

        playerObj = GameObject.Find("kings");
        DustStorm = GameObject.Find("DustStorm");
        bgmObject = GameObject.Find("BGM");
        socicalCamRoot = GameObject.Find("SocialCameraRoot");

        m_bPlayJingleOneShot = false;

        Assert.IsNotNull(playerObj, "Player Object Is Null!!!!!!!!!!!!!!!!!!!!");    
	}
	
	// Update is called once per frame
	new void Update () {

        if( GameFadeManager.Instance.IsFade() )
        {
            return;
        }

        /*
        if( Input.GetButtonDown("R1") )
        {
            VRManager.Instance.ChangeVRMode();       
        }
        */

        cameraMngr.ChangeCameraMode();

        if( Input.GetButtonDown("R3") )
        {
            GameSceneManager.Instance.ChangeScene(GameModeData.GAMEMODE.GAME_MODE_BOOT);
        }

        // ステージの状態を更新
        UpdateStageState();

        CountOneFrame();
	}


    public void PlayDustStorm(bool bPlay)
    {
        if( !particleBase ) return;

        if( bPlay )
        {
            particleBase.Play();
        }
        else
        {
            particleBase.Stop();
        }
    }

    public bool IsPlayDustStorm()
    {
        if( !particleBase ) return false;

        return particleBase.IsPlay();
    }

    private void CountOneFrame()
    {
        m_frame += Time.deltaTime;
        if( m_frame >= 1.0f )
        {
            m_frame = 0.0f;
            m_counterSec++;
        }
    }
  

    //---------------------------------------------------------------
    /*
        @brief      ステートの変更
    */
    //---------------------------------------------------------------
    private void ChangeState(StageState state)
    {
        m_state = state;
        m_subStep = 0;
        m_frame = 0.0f;
        m_counterSec = 0;
    }

    //---------------------------------------------------------------
    /*
        @brief      ステージの状態を更新
    */
    //---------------------------------------------------------------
    void UpdateStageState()
    {
        VRUIManager uiManager = cameraMngr.FpsCameraObj.GetComponent<VRUIManager>();

        if( GameSceneManager.Instance.NowLoading )
        {
            return;
        }

        switch( m_state )
        {
        case StageState.STATE_PREPARE:

            if( !m_bPlayJingleOneShot )
            {
                bgmObject.GetComponent<BGMCtrl>().PlayJingle();
                m_bPlayJingleOneShot = true;
            }

            if( m_counterSec >= 3 )
            {
                uiManager.ScrCountDown.StartCountDown();
                ChangeState( StageState.STATE_READY );
            }

            break;
        case StageState.STATE_READY:

            if( uiManager.ScrCountDown.LetsBonyage )
            {
                // 最初は自動で吹っ飛ぶ
                playerObj.GetComponent<CharAction>().FirstBlowoff();

                bgmObject.GetComponent<BGMCtrl>().PlayBGM();

                ChangeState(StageState.STATE_INGAME);
            }

            break;
        case StageState.STATE_INGAME:

            if( playerObj.GetComponent<CharAction>().IsGoal )
            {
                bgmObject.GetComponent<BGMCtrl>().StopBGM();
                uiManager.ScrGoalUI.StartGoalUI();

                ChangeState(StageState.STATE_GOAL);
            }
            else if( playerObj.GetComponent<CharAction>().IsGameOver )
            {
                ChangeState(StageState.STATE_GAMEOVER);
            }

            break;

        case StageState.STATE_GOAL:

            // とりあえず10秒ほど経過したら
            if( m_counterSec > 10 )
            {
                ChangeState(StageState.STATE_RESET);
            }

            break;

        case StageState.STATE_RESET:

            bool resetDone = OnEasyReset();
            if( resetDone )
            {
                ChangeState(StageState.STATE_PREPARE);
            }
            break;

        default:
            break;
        }
    }

    //---------------------------------------------------------------
    /*
        @brief      リセット処理
    */
    //---------------------------------------------------------------
    private bool OnEasyReset()
    {
        bool ret = false;

        const int RESET_FADEOUTSTART = 0;
        const int RESET_FADEOUTWAIT = 1;
        const int RESET_FADEINWAIT = 3;

        VRUIManager uiManager = cameraMngr.FpsCameraObj.GetComponent<VRUIManager>();

        switch( m_subStep )
        {
        // フェードアウト開始
        case RESET_FADEOUTSTART:
            GameSceneManager.Instance.StartFadeOut(24);
            m_subStep = RESET_FADEOUTWAIT;
            break;
        case RESET_FADEOUTWAIT:
            if( !GameSceneManager.Instance.IsFade )
            {
                /*
                    ToDo リセット処理
                */
                playerObj.GetComponent<CharAction>().ResetPosition(true);
                uiManager.ScrGoalUI.StopGoalUI();
                socicalCamRoot.GetComponent<DisplayCameraManager>().OnEasyReset();

                m_bPlayJingleOneShot = false;

                GameSceneManager.Instance.StartFadeIn(24);
                m_subStep = RESET_FADEINWAIT;
            }
            break;
        case RESET_FADEINWAIT:

            if( !GameSceneManager.Instance.IsFade )
            {
                ret = true;
            }

            break;
        }

        return ret;
    }


}
