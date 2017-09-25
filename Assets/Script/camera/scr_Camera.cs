using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Camera : MonoBehaviour {

    private Camera  m_camera;

    private Quaternion totalRotation;

    public const int ROTATE_UNIT_ANGLE = 5;
    private float angle_yaw;
    private float angle_pitch;
    public float Angle_Yaw { get { return angle_yaw; } }
    public float Anble_Pitch { get { return angle_pitch; } }

    private GameObject targetObj = null;
    private Vector3 targetOffset;
    public float offsetLength = 20.0f;

	// Use this for initializationc
	void Start () {
        m_camera = GetComponent<Camera>();
        
        totalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        angle_yaw = 0.0f;
        angle_pitch = 0.0f;

        targetObj = GameObject.Find("test_cube");
        targetOffset = -(targetObj.transform.forward);
        targetOffset *= offsetLength;
	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetButtonDown("Option") )
        {
            ResetCamera();
        }

        float rh = Input.GetAxis("Horizontal2");
        float rv = Input.GetAxis("Vertical2");

        angle_yaw += rh * ROTATE_UNIT_ANGLE;
        angle_pitch += rv * ROTATE_UNIT_ANGLE;

        totalRotation = Quaternion.Euler(angle_pitch, angle_yaw, 0.0f);
        m_camera.transform.rotation = totalRotation;
	}

    void LateUpdate()
    {
        
        if( targetObj )
        {
            Vector3 targetPos = targetObj.transform.position;

            targetOffset = -targetObj.transform.forward;
            targetOffset.y = (targetObj.transform.up.y / 2.0f);
            targetOffset *= offsetLength;
            targetPos += targetOffset;

            m_camera.transform.position = targetPos;
//            m_camera.transform.rotation = targetObj.transform.rotation;            
        }
        
        /*
        {
            Vector3 forward = Vector3.Normalize(m_camera.transform.forward);
            Vector3 right = Vector3.Normalize(m_camera.transform.right);
            Vector3 up = Vector3.Normalize(m_camera.transform.up);

            string infoStr = "";
            infoStr += "Forward:" + forward.ToString() + "\n";
            infoStr += "Right:" + right.ToString() + "\n";
            infoStr += "Up:" + up.ToString() + "\n";
            GameObject.Find("Text").GetComponent<scr_GUIText>().AddText(infoStr);
        }
        */
    }

    void ResetCamera()
    {
        totalRotation = Quaternion.Euler(angle_pitch, angle_yaw, 0.0f);
        transform.rotation = totalRotation;
    }
}
