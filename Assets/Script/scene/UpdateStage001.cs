using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class UpdateStage002 : UpdateBase
{
    private GameObject DustStorm;
    public ParticleSystemBase particlBase
    {
        get { return DustStorm.GetComponent<ParticleSystemBase>(); }
    }

    void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        cameraMngr = new CameraManager();
        cameraMngr.Initialize();
        GameFadeManager.Instance.SetupVRMode(Camera.main.name);
    }

	// Use this for initialization
	void Start () {

        DustStorm = GameObject.Find("DustStorm");
        particlBase.Play();
	}
	
	// Update is called once per frame
	void Update () {

        if( Input.GetButtonDown("R1") )
        {
            VRManager.Instance.ChangeVRMode();       
        }

        cameraMngr.ChangeCameraMode();

        if( Input.GetButtonDown("R3") )
        {
            GameSceneManager.Instance.ChangeScene(GameModeData.GAMEMODE.GAME_MODE_BOOT);
        }
	}

}
