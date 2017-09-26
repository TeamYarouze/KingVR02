using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****************************************************************************/
/*
    @brief      TPS風カメラ
    @author     Y.Suzuki
    @since      2017/03/16
    @note       ターゲットOBJの周りを回転するTPS風カメラ
*/
/*****************************************************************************/
public class scr_CameraTPS : MonoBehaviour {

    private Camera  m_camera;
//    private Quaternion totalRotation;

    public const int ROTATE_UNIT_ANGLE = 5;
    public const float TARGET_DISTANCE = 15.0f;

    private Vector3 m_offsetVector;
    private float angle_yaw;
    private float angle_pitch;
    public float Angle_Yaw { get { return angle_yaw; } }
    public float Anble_Pitch { get { return angle_pitch; } }

    private GameObject m_targetObj = null;
    private Vector3 m_targetPos;

    private float m_targetDistance;

	// Use this for initialization
	void Start () {
        m_camera = GetComponent<Camera>();
		m_targetObj = GameObject.Find("kings");
        m_targetPos = m_targetObj.transform.position;

//        totalRotation = Quaternion.identity;

        m_targetDistance = TARGET_DISTANCE;
        CalcOffsetVector(out m_offsetVector);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float rh = Input.GetAxis("Horizontal2");
        float rv = Input.GetAxis("Vertical2");

        // ターゲットObjが動いた分だけカメラも動く
        m_camera.transform.position += m_targetObj.transform.position - m_targetPos;
		m_targetPos = m_targetObj.transform.position;

        AroundRotate(rv, rh);
	}

    void LateUpdate()
    {
        /*
        {
            string infoStr = "";
            infoStr += "Angle Yaw : " + angle_yaw + "\n";
            infoStr += "Angle Pitch : " + angle_pitch + "\n";
            scr_GUIText.instance.AddText(infoStr);
        }
        */
    }

    //-----------------------------------------------------------------
    /*
        @brief  ターゲットOBJの周りを回転する
    */
    //-----------------------------------------------------------------
    void AroundRotate(float v, float h)
    {
        angle_yaw += h * ROTATE_UNIT_ANGLE;
        angle_pitch += v * ROTATE_UNIT_ANGLE;
        if( angle_pitch >= 45.0f ) angle_pitch = 45.0f;
        if( angle_pitch <= -20.0f ) angle_pitch = -20.0f;

        Vector3 yawAxis = Vector3.Scale(m_targetPos, Vector3.up).normalized;
        Quaternion rot = Quaternion.Euler(angle_pitch, angle_yaw, 0.0f);

        Vector3 rotatedVector = rot * m_offsetVector;
        Vector3 lookAt = m_targetPos;
        lookAt.y += 5.0f;
        
        m_camera.transform.position = m_targetPos + (rotatedVector * m_targetDistance);
        m_camera.transform.LookAt(lookAt);
    }

    //--------------------------------------------------------------------
    /*
        @brief      ターゲットOBJへの単位ベクトルを取得する
        @param[out] Vector3 vOut ターゲットOBJへのベクトル
    */
    //--------------------------------------------------------------------
    void CalcOffsetVector(out Vector3 vOut)
    {
        vOut = Vector3.Normalize(m_camera.transform.position - m_targetPos);
    }
}
