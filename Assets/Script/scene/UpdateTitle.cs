using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class UpdateTitle : UpdateBase {

    private bool bNext;

    private int m_step;
    private AudioSource audioSource;
    private float m_frame;

    private AudioSource bgm;

    new void Awake()
    {
        base.Awake();
    }

	/**
     *  初期化処理です
     */
	new void Start () {
		// 初期化処理をここに書きます
        // 初期化処理なので実行されるのはシーン開始時の1回だけです
        if( VRSettings.enabled )
        {
            VRManager.Instance.ToggleHMDViewOnMonitor(true);
        }


        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        if( !VRSettings.enabled )
        {
            VRManager.Instance.ChangeVRMode();
        }

        m_step = 0;
        bNext = false;
        bgm.Play();
	}
	
	/**
     * 毎フレームの更新処理です
     */
	new void Update () {
		// 毎フレームの更新処理をここに書きます

        if( GameSceneManager.Instance.NowLoading )
        {
            return;
        }

        switch( m_step )
        {
        case 0:
            if( !bNext )
            {

                if( Input.GetButtonDown("Circle") || Input.GetButtonDown("Cross")
                    || Input.GetButtonDown("Square") || Input.GetButtonDown("Triangle") )
                {
                    bgm.Stop();
                    audioSource.Play();

                    GameSceneManager.Instance.ChangeScene(GameModeData.GAMEMODE.GAME_MODE_STAGE);
                    bNext = true;
                    m_step++;
                }
            }
            break;
        case 1:

            /*
            m_frame += Time.deltaTime;
            if( m_frame > 1.0f )
            {
                GameSceneManager.Instance.ChangeScene(GameModeData.GAMEMODE.GAME_MODE_STAGE);
                m_step = 2;
            }
            */
            break;
        case 2:
        default:
            break;
        }
	}
}
