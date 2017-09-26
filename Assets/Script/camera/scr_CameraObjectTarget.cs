using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****************************************************************************/
/*
    @brief      デバック用モデルビューカメラ
    @author     Y.Suzuki
    @since      2017/03/16
    @note       ターゲットOBJの周りを回転するTPS風カメラ
*/
/*****************************************************************************/

public class scr_CameraObjectTarget : MonoBehaviour {

    private Camera  m_camera;

    public const int ROTATE_UNIT_ANGLE = 5;
    public const float TARGET_DISTANCE = 15.0f;

    private Vector3 m_offsetVector;
    private float angle_yaw;
    private float angle_pitch;
    public float Angle_Yaw { get { return angle_yaw; } }
    public float Anble_Pitch { get { return angle_pitch; } }

    private Vector3 m_offsetPos;
    
    [SerializeField]
    private GameObject m_targetObj;

    private Vector3 m_targetPos;

    private float m_targetDistance;

    private float target_yaw;
    private float target_pitch;

	// Use this for initialization
	void Start () {

        if( m_targetObj == null )
        {
            Debug.LogError("Need Setup Target Object!!!!!");
            return;
        }

		m_camera = GetComponent<Camera>();
        m_targetPos = m_targetObj.transform.position;

        m_targetDistance = TARGET_DISTANCE;
        CalcOffsetVector(out m_offsetVector);

        m_offsetPos = Vector3.zero;

        target_yaw = 0.0f;
        target_pitch = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        float lh = Input.GetAxis("Horizontal");
        if( lh > -0.1f && lh < 0.1f ) lh = 0.0f;
        float lv = Input.GetAxis("Vertical");
        if( lv > -0.1f && lv < 0.1f ) lv = 0.0f;
        float rh = Input.GetAxis("Horizontal2");
        if( rh > -0.1f && rh < 0.1f ) rh = 0.0f;
        float rv = Input.GetAxis("Vertical2");
        if( rv > -0.1f && rv < 0.1f ) rv = 0.0f;

        float dh = Input.GetAxis("D-Horizontal");
        float dv = Input.GetAxis("D-Vertical");

        float RShoulder = Input.GetAxis("R2");
        float LShoulder = Input.GetAxis("L2");
        float L2 = 0.0f;
        float R2 = 0.0f;
#if UNITY_PS4
        if( LShoulder < -0.25f )
        {
            L2 = -LShoulder;
        }

        if( RShoulder > 0.25f )
        {
            R2 = RShoulder;
        }
        else if( RShoulder < -0.25f )
        {
            L2 = -RShoulder;
        }
#else
        if( LShoulder < -0.25f ) L2 = -LShoulder;
        if( RShoulder > 0.25f ) R2 = RShoulder;
#endif//

        ChangeTargetDistance(L2, R2);
        UpdateOffsetPosition(dv, dh);
        AroundRotate(rv, rh);

        // ターゲットOBJを回転
        RotateTargetObj(lv, lh);
	}

    //-----------------------------------------------------------------
    /*
        @brief  ターゲットOBJの周りを回転する
    */
    //-----------------------------------------------------------------
    void AroundRotate(float v, float h)
    {
        angle_yaw += h * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();
        angle_pitch += v * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();
        if( angle_pitch >= 60.0f ) angle_pitch = 60.0f;
        if( angle_pitch <= -60.0f ) angle_pitch = -60.0f;

        Vector3 yawAxis = Vector3.Scale(m_targetPos, Vector3.up).normalized;
        Quaternion rot = Quaternion.Euler(angle_pitch, angle_yaw, 0.0f);

        Vector3 rotatedVector = rot * m_offsetVector;
        Vector3 lookAt = m_targetPos + m_offsetPos;
        
        m_camera.transform.position = m_targetPos + m_offsetPos + (rotatedVector * m_targetDistance);
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

    //--------------------------------------------------------------------
    /*
        @brief      ターゲットOBJへのオフセット座標を更新する
    */
    //--------------------------------------------------------------------
    void UpdateOffsetPosition(float v, float h)
    {
        float moveX = h * 0.1f * GameDefine.FPSDeltaScale();
        float moveY = v * 0.1f * GameDefine.FPSDeltaScale();

        m_offsetPos.x += moveX;
        m_offsetPos.y += moveY;
    }

    void ChangeTargetDistance(float forward, float back)
    {
        m_targetDistance -= (forward * 0.25f * GameDefine.FPSDeltaScale());
        if( m_targetDistance <= 3.0f ) m_targetDistance = 3.0f;
        m_targetDistance += (back * 0.25f * GameDefine.FPSDeltaScale());
    }

    //--------------------------------------------------------------------
    /*
        @brief      ターゲットOBJを回転させる
    */
    //--------------------------------------------------------------------
    void RotateTargetObj(float v, float h)
    {
        target_yaw += h * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();
        target_pitch += v * ROTATE_UNIT_ANGLE * GameDefine.FPSDeltaScale();

        Quaternion rot = Quaternion.Euler(target_pitch, target_yaw, 0.0f);

        m_targetObj.transform.rotation = rot;
    }
    

    void OnGUI()
    {
        GUI.Box(new Rect(1, 1, 300, 100), "Pos: " + transform.position.ToString() + "\n" + 
                                          "Distance : " + m_targetDistance + "\n" +
                                          "LR : " + Input.GetAxis("R2") );
    }
    
}
