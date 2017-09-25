using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
#if UNITY_PS4
using UnityEngine.PS4.VR;
#endif  //

public class scr_CameraFPS : MonoBehaviour {

    private Camera  m_camera;
    private Quaternion totalRotation;

    public const int ROTATE_UNIT_ANGLE = 2;
    public float offsetHeight;
    public float offsetForward;

    private float angle_yaw;
    private float angle_pitch;
    public float Angle_Yaw { get { return angle_yaw; } }
    public float Anble_Pitch { get { return angle_pitch; } }

    private GameObject m_targetObj = null;

	// Use this for initialization
	void Start ()
    {
        m_camera = GetComponent<Camera>();
        m_targetObj = GameObject.Find("kings");

        totalRotation = Quaternion.identity;
        offsetHeight = 2.0f;
        offsetForward = 2.0f;

        angle_yaw = 180.0f;
        angle_pitch = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void LateUpdate()
    {
        if( !VRSettings.enabled )
        {
            float rh = Input.GetAxis("Horizontal2");
            float rv = Input.GetAxis("Vertical2");

            RotateCamera(rv, rh);
        }

        transform.position = transform.parent.position;

        /*
        {
            string infoStr = "";
            infoStr += "Angle Yaw : " + angle_yaw + "\n";
            infoStr += "Angle Pitch : " + angle_pitch + "\n";
            scr_GUIText.instance.AddText(infoStr);
        }
        */
    }

    //---------------------------------------------------------------
    /*
        @brief      カメラの移動
    */
    //---------------------------------------------------------------
    public void MoveCamera()
    {
        Vector3 position = m_targetObj.transform.position;

        position.y += offsetHeight;

        m_camera.transform.position = position;
    }

    //---------------------------------------------------------------
    /*
        @brief      カメラの回転
    */
    //---------------------------------------------------------------
    public void RotateCamera(float v, float h)
    {
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

}
