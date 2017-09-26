using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class CameraManager
{
    private Camera socialCamera = null;
    private Camera freeLookCamera = null;
    private Camera fpsCamera = null;

    private GameObject socialCameraObj;
    private GameObject fpsCameraObj;
    private GameObject freeLookCameraObj;

    public GameObject FpsCameraObj
    {
        get { return fpsCameraObj; }
    }
    public GameObject SocialCameraObj
    {
        get { return socialCameraObj; }
    }
    public GameObject FreeLookCameraObj
    {
        get { return freeLookCameraObj; }
    }

    public enum UseCameraType
    {
        USE_CAMERA_SOCIAL = 0,
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


    public void Initialize()
    {
        socialCameraObj = GameObject.Find("SocialScreenCamera");
        if( socialCameraObj )
        {
		    socialCamera = GameObject.Find("SocialScreenCamera").GetComponent<Camera>();
            if( !socialCamera )
            {
                Debug.Log("MainCamera Object Not Find\n");
            }
        }

        freeLookCameraObj = GameObject.Find("FreeCamera");
        if( freeLookCameraObj )
        {
            freeLookCamera = GameObject.Find("FreeCamera").GetComponent<Camera>(); 
            if( !freeLookCamera )
            {
                Debug.Log("FreeLookCamera Object Not Find\n");
            }
        }

        fpsCameraObj = GameObject.Find("FPSCamera");
        if( fpsCameraObj )
        {
            fpsCamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
            if( !fpsCamera )
            {
                Debug.Log("FPS Camera Object Not Find\n");
            }
        }

        cameraType = UseCameraType.USE_CAMERA_INVALID;
        EnableFPSCamera();
    }

    public void ChangeCameraMode()
    {
        if( Input.GetKeyDown(KeyCode.C) )
        {
            cameraType++;
            if( cameraType >= UseCameraType.USE_CAMERA_NUM )
            {
                cameraType = UseCameraType.USE_CAMERA_SOCIAL;
            }

            if( cameraType == UseCameraType.USE_CAMERA_SOCIAL )
            {
                EnableSocialCamera();
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

     public void EnableSocialCamera()
    {
        if( socialCamera )
        {
            socialCamera.enabled = true;
            if( !VRSettings.enabled )
            {
                cameraType = UseCameraType.USE_CAMERA_SOCIAL;
            }
        }
        
        if( !VRSettings.enabled )
        {
            if( freeLookCamera )
            {
                freeLookCamera.enabled = false;
            }
            if( fpsCamera )
            {
                fpsCamera.enabled = false;
            }
        }
    }

    public void EnableFreeLookCamera()
    {
        if( socialCamera )
        {
            socialCamera.enabled = false;
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
        if( socialCamera )
        {
            if( VRSettings.enabled )
            {
                socialCamera.enabled = true;
            }
            else
            {
                socialCamera.enabled = false;
            }
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

    public Camera GetCurrentCameraComponent()
    {
        switch( cameraType )
        {
        case UseCameraType.USE_CAMERA_FPS:
            return fpsCamera;
        case UseCameraType.USE_CAMERA_FREELOOK:
            return freeLookCamera;
        case UseCameraType.USE_CAMERA_SOCIAL:
            return socialCamera;
        }

        return null;
    }
}
