using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class DisplayCameraManager : MonoBehaviour {

    private static int CAMERA_NUM = 8;

    public GameObject[] displayCamera = new GameObject[CAMERA_NUM];

    private int m_camIndex;


    private Camera  m_camera;
    private Quaternion totalRotation;

    public const int ROTATE_UNIT_ANGLE = 1;
    public const float TARGET_DISTANCE = 15.0f;

    public float fov;

    private Vector3 m_offsetVector;
    private float angle_yaw;
    private float angle_pitch;
    public float Angle_Yaw { get { return angle_yaw; } }
    public float Anble_Pitch { get { return angle_pitch; } }

    private struct CameraDefaultData
    {
//        public float yaw;
//        public float pitch;
        public float fov;
        public Quaternion rotation;
    };
    CameraDefaultData[] defData = new CameraDefaultData[CAMERA_NUM];

	// Use this for initialization
	void Start () {
		
        m_camIndex = 0;
        ChangeDisplayCamera();

        for(int i = 0; i < CAMERA_NUM; i++)
        {
 //           defData[i].yaw = displayCamera[i].GetComponent<Camera>().transform.rotation.eulerAngles.y;
 //           defData[i].pitch = displayCamera[i].GetComponent<Camera>().transform.rotation.eulerAngles.x;
            defData[i].fov = displayCamera[i].GetComponent<Camera>().fieldOfView;
            defData[i].rotation = displayCamera[i].GetComponent<Camera>().transform.rotation;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
        if( !VRSettings.enabled )
        {
//            return;
        }


        float lh = Input.GetAxis("Horizontal");
        if( lh > -0.1f && lh < 0.1f ) lh = 0.0f;
        float lv = Input.GetAxis("Vertical");
        if( lv > -0.1f && lv < 0.1f ) lv = 0.0f;
        float rh = Input.GetAxis("Horizontal2");
        if( rh > -0.1f && rh < 0.1f ) rh = 0.0f;
        float rv = Input.GetAxis("Vertical2");
        if( rv > -0.1f && rv < 0.1f ) rv = 0.0f;

        if( m_camera )
        {
            RotateCamera(-rv, rh);
            SettingFov(-lv);
        }


        if( Input.GetButtonDown("L1") )
        {
            m_camIndex--;
            if( m_camIndex < 0 ) m_camIndex = CAMERA_NUM-1;

            ChangeDisplayCamera();
        }
        else if( Input.GetButtonDown("R1") )
        {
            m_camIndex++;
            if( m_camIndex >= CAMERA_NUM ) m_camIndex = 0;
            ChangeDisplayCamera();
        }
	}

    private void ChangeDisplayCamera()
    {
        if( !VRSettings.enabled )
        {
            for(int i = 0; i < CAMERA_NUM; i++)
            {
                displayCamera[i].GetComponent<Camera>().enabled = false;
            }
            m_camera = null;
            angle_yaw = 0.0f;
            angle_pitch = 0.0f;
            totalRotation = Quaternion.identity;
            return;
        }

        for(int i = 0; i < CAMERA_NUM; i++)
        {
            if( i == m_camIndex )
            {
                displayCamera[i].GetComponent<Camera>().enabled = true;
                m_camera = displayCamera[i].GetComponent<Camera>();
                fov = m_camera.fieldOfView;
                angle_yaw = m_camera.transform.rotation.eulerAngles.y;
                angle_pitch = m_camera.transform.rotation.eulerAngles.x;
                totalRotation = m_camera.transform.rotation;
            }
            else
            {
                displayCamera[i].GetComponent<Camera>().enabled = false;
            }
        }
    }

    //---------------------------------------------------------------
    /*
        @brief      カメラの回転
    */
    //---------------------------------------------------------------
    public void RotateCamera(float v, float h)
    {
        if( !m_camera )
        {
            return;
        }

        angle_yaw += h * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();
        if( angle_yaw > 360.0f )
        {
            angle_yaw -= 360.0f;
        }
        if( angle_yaw < 0 )
        {
            angle_yaw += 360.0f;
        }
        angle_pitch += v * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();
        if( angle_pitch > 360.0f )
        {
            angle_pitch -= 360.0f;
        }
        if( angle_pitch < 0.0f )
        {
            angle_pitch += 360.0f;
        }

        totalRotation = Quaternion.Euler(angle_pitch, angle_yaw, 0.0f);
        m_camera.transform.rotation = totalRotation;
    }

    public void SettingFov(float lv)
    {
        if( !m_camera )
        {
            return;
        }

        fov += lv;
        if( fov < 10.0f ) fov = 10.0f;
        if( fov > 60.0f ) fov = 60.0f;

        m_camera.fieldOfView = fov;

    }

    public void OnEasyReset()
    {

        for(int i = 0; i < CAMERA_NUM; i++)
        {
            displayCamera[i].GetComponent<Camera>().fieldOfView = defData[i].fov;
            displayCamera[i].GetComponent<Camera>().transform.rotation = defData[i].rotation;
        }

        m_camIndex = 0;
        ChangeDisplayCamera();
    }

    /*
    public void OnGUI()
    {
        GUI.TextField(new Rect(1600.0f, 100.0f, 300, 180), "CAMERA No: " + m_camIndex);
    }
    */
}
