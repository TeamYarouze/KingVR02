using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
#if UNITY_PS4
using UnityEngine.PS4;
#endif  //

public class scr_SceneManager : Singleton<scr_SceneManager> {

    private Camera mainCamera = null;
    private Camera freeLookCamera = null;
    private Camera fpsCamera = null;

    private GameObject targetObj = null;

    private bool bSetupHMDDevice;
    public bool IsSetupHMDDevice
    {
        get {   return bSetupHMDDevice;     }
    }

    public enum UseCameraType
    {
        USE_CAMERA_MAIN = 0,
        USE_CAMERA_FREELOOK,
        USE_CAMERA_FPS,
        USE_CAMERA_NUM,

        USE_CAMERA_INVALID = 0xff,

        USE_CAMERA_MAX,
    };
    private UseCameraType cameraType;
    public UseCameraType CameraType
    {
        set { cameraType = value; }
        get { return cameraType; }
    }
   
    private int m_vrSettintStep;

    public new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
        if( GameObject.Find("SocialScreenCamera") )
        {
		    mainCamera = GameObject.Find("SocialScreenCamera").GetComponent<Camera>();
            if( !mainCamera )
            {
                Debug.Log("MainCamera Object Not Find\n");
            }
        }

        if( GameObject.Find("FreeCamera") )
        {
            freeLookCamera = GameObject.Find("FreeCamera").GetComponent<Camera>(); 
            if( !freeLookCamera )
            {
                Debug.Log("FreeLookCamera Object Not Find\n");
            }
        }

        if( GameObject.Find("FPSCamera") )
        {
            fpsCamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
            if( !fpsCamera )
            {
                Debug.Log("FPS Camera Object Not Find\n");
            }
        }
                
        bSetupHMDDevice = false;
        m_vrSettintStep = -1;
//        EnableMainCamera();
        EnableFPSCamera();

        if( VRSettings.enabled )
        {
            VRManager.Instance.BeginVRSetup();
        }

        /*
        cameraType = UseCameraType.USE_CAMERA_INVALID;
        EnableMainCamera();
        targetObj = GameObject.Find("unitychan");
        if( targetObj == null )
        {
            Debug.Log("unitychan isnot Find\n");
        }
        targetObj.GetComponent<CharAction>().SetMoveMode(CharAction.ACTION_MODE.ACT_MODE_BLOWOFF);
        */
	}
	
	// Update is called once per frame
	void Update () {


        if( Input.GetButtonDown("R1") )
        {
            ProcSetupVRMode();
        }

        if( VRSettings.enabled && Input.GetButtonDown("Triangle") )
        {
            VRManager.Instance.ToggleHMDViewOnMonitor();
        }

		if( Input.GetKeyDown(KeyCode.C) )
        {
            cameraType++;
            if( cameraType >= UseCameraType.USE_CAMERA_NUM )
            {
                cameraType = UseCameraType.USE_CAMERA_MAIN;
            }

            if( cameraType == UseCameraType.USE_CAMERA_MAIN )
            {
                EnableMainCamera();
            }
            else if( cameraType == UseCameraType.USE_CAMERA_FREELOOK )
            {
                EnableFreeLookCamera();
            }
            else if( cameraType == UseCameraType.USE_CAMERA_FPS )
            {
                EnableFPSCamera();
            }
        }
   }

    void LateUpdate()
    {

        {
            string cameraStr = "CameraType : ";
            if (cameraType == UseCameraType.USE_CAMERA_MAIN)
            {
                cameraStr += "Main\n";
            }
            else if (cameraType == UseCameraType.USE_CAMERA_FREELOOK)
            {
                cameraStr += "FreeLook\n";
            }
            else if( cameraType == UseCameraType.USE_CAMERA_FPS)
            {
                cameraStr += "FPS\n";
            }
            else
            {
                cameraStr += "Invalid\n";
            }

            cameraStr += "Show Hmd" + ((VRSettings.showDeviceView)?"ON":"OFF") + "\n";
            scr_GUIText.instance.AddText(cameraStr);
        }
    }

    public void SetCameraMode(UseCameraType type)
    {
        if( type == UseCameraType.USE_CAMERA_MAIN )
        {
            EnableMainCamera();
        }
        else if( type == UseCameraType.USE_CAMERA_FREELOOK )
        {
            EnableFreeLookCamera();
        }
        else if( type == UseCameraType.USE_CAMERA_FPS )
        {
            EnableFPSCamera();
        }      
    }

    public void EnableMainCamera()
    {
        if( mainCamera )
        {
            mainCamera.enabled = true;
            cameraType = UseCameraType.USE_CAMERA_MAIN;
        }
        if( freeLookCamera )
        {
            freeLookCamera.enabled = false;
        }
        if( fpsCamera )
        {
            fpsCamera.enabled = false;
        }
    }

    public void EnableFreeLookCamera()
    {
        if( mainCamera )
        {
            mainCamera.enabled = false;
        }

        if( freeLookCamera )
        {
            freeLookCamera.enabled = true;
            cameraType = UseCameraType.USE_CAMERA_FREELOOK;
        }
        if( fpsCamera )
        {
            fpsCamera.enabled = false;
        }
    }

    public void EnableFPSCamera()
    {
        if( mainCamera )
        {
            mainCamera.enabled = false;
        }
        if( freeLookCamera )
        {
            freeLookCamera.enabled = false;
        }
        if( fpsCamera )
        {
            fpsCamera.enabled = true;
            cameraType = UseCameraType.USE_CAMERA_FPS;
        }
    }

    public void ProcSetupVRMode()
    {
        Debug.Log("------------- Scene Manager Begin SetupHMD Device");
        VRManager.Instance.SetupHMDDevice();
    }
}
