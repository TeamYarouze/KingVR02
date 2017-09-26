using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class UpdateBootSequence : UpdateBase {

    private float counter;
    private int m_step;

    void OnEnable()
    {
        GameFadeManager.Instance.SetupVRMode(Camera.main.name);
    }

	// Use this for initialization
	new void Start () {
		counter = 0;
        m_step = 0;
	}

	// Update is called once per frame
	new void Update () {

        if( GameFadeManager.Instance.IsFade() )
        {
            return;
        }

        switch( m_step )
        {
        case 0:

            m_step++;

            break;
        case 1:

            m_step++;

            break;
        case 2:

            counter += Time.deltaTime;
		
            if( counter >= 1.0f )
            {
                // シーン遷移テスト!!!!!
                GameSceneManager.Instance.ChangeScene(GameModeData.GAMEMODE.GAME_MODE_TITLE);
                m_step = 3;
            }
            break;
        default:
            break;
        }

	}
}
