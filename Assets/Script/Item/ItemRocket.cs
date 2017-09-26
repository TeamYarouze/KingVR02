using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
#endif      //

public class ItemRocket : ItemBase {

    [SerializeField]
    private float RocketInitialAngle = 60.0f;
    [SerializeField]
    private float RocketInitialPower = 150.0f;
//    private float RocketInitialPower = (200.0f * 1000.0f) / (60.0f*60.0f);
    [SerializeField]
    private float RocketGravity = 10.0f;

    [SerializeField]
    private int ReloadFirstStep = 2;
    [SerializeField]
    private int ReloadSecondStep = 4;
    [SerializeField]
    private int ReloadeThiredStep = 6;

    [SerializeField]
    private float ReloadShortAngle = 45.0f;
    [SerializeField]
    private float ReloadShortPower = 50.0f;
    [SerializeField]
    private float ReloadMiddleAngle = 60.0f;
    [SerializeField]
    private float ReloadMiddlePower = 30.0f;
    [SerializeField]
    private float ReloadLongAngle = 75.0f;
    [SerializeField]
    private float ReloadLongPower = 10.0f;

    [SerializeField]
    private int ReloadTimer = 3;
    [SerializeField]
    private float AddMaxAngle = 40.0f;
    [SerializeField]
    private float AddMaxPower = 30.0f;
    [SerializeField]
    private float InitialRocketAngle = 60.0f;
    [SerializeField]
    private float InitialRocketPower = 100.0f;

    private float m_rocketPower;
    private scr_VRCameraRoot scrCamera;

    private Vector3 rotateAxis;

    private EffectManager effectMngr = null;
 
    private AudioSource audioSource;

    public AudioClip seExplosion;
    public AudioClip seAfterBurner;
    public AudioClip seCharge;

    private bool m_bChargeStart;

    private UpdateStage stageUpdater;

    public enum RocketSE
    {
        SE_EXPLOSION,
        SE_AFTER_BURNER,
        SE_CHARGE,
    };

	// Use this for initialization
	new public void Start () {
        base.Start();

        stageUpdater = GameObject.Find("SceneUpdater").GetComponent<UpdateStage>();

        m_Angle = RocketInitialAngle;
        m_Power = RocketInitialPower;
        m_Gravity = RocketGravity;

        m_type = GameDefine.ITEM_TYPE.ITEM_TYPE_ROCKET;
        m_state = EItemUseState.ITEM_STAT_READY;
        m_useType = (GameDefine.ItemUseType_UseAgain | GameDefine.ItemUseType_Reload | GameDefine.ItemUseType_BtnTrigger);

        scrCamera = GameObject.FindWithTag("CameraRoot").GetComponent<scr_VRCameraRoot>();
        rotateAxis = Vector3.zero;
        m_rocketPower = 0.0f;

        effectMngr = GameObject.Find("EffectManager").GetComponent<EffectManager>();

        audioSource = GetComponent<AudioSource>();
        m_bChargeStart = false;
	}
	
	// Update is called once per frame
	new public void Update ()
    {
		if( !IsAttachedObject() )
        {
            return;
        }

        if( stageUpdater.CurrentState != UpdateStage.StageState.STATE_INGAME )
        {
            return;
        }
        

        UpdateReloadTime();

//        OnFire();
        
        OnFire2();
        // ウンコー
        if( Input.GetButton("Triangle") )
        {
            m_state = EItemUseState.ITEM_STAT_READY;
        }
	}

    //---------------------------------------------------------------
    /*
        @brief      更新
    */
    //---------------------------------------------------------------
    public void FixedUpdate()
    {
        if( !IsAttachedObject() )
        {
            return;
        }
    }

    //---------------------------------------------------------------
    /*
        @brief      発射処理
    */
    //---------------------------------------------------------------
    new public bool OnFire()
    {
        if( Input.GetButtonDown("Circle") )
        {
            Vector3 outVelocity;
            Vector3 vForward = Vector3.zero;

            float power = 0.0f;

            switch( m_state )
            {
            case EItemUseState.ITEM_STAT_READY:
                vForward = attachedObject.transform.forward;
                m_Angle = RocketInitialAngle;
                m_Power = RocketInitialPower;
                power = (m_Power * 1000.0f) / (60.0f * 60.0f);
                break;
            case EItemUseState.ITEM_STAT_USING:
            
                vForward = objScript.RigidBody.velocity;
                if( m_reloadTime >= ReloadeThiredStep )
                {
                    m_Angle = ReloadLongAngle;
                    m_Power = ReloadLongPower;
                }
                else if( m_reloadTime >= ReloadSecondStep )
                {
                    m_Angle = ReloadMiddleAngle;
                    m_Power = ReloadMiddlePower;
                }
                else if( m_reloadTime >= ReloadFirstStep )
                {
                    m_Angle = ReloadShortAngle;
                    m_Power = ReloadShortPower;
                }
                else
                {
                    return false;
                }

                power = objScript.RigidBody.velocity.magnitude + m_Power;

                break;
            }

            bool bLaunch = SetupRocketOrbit(out outVelocity, vForward, m_Angle, power);

            if( bLaunch )
            {
                objScript.SetupBlowoffParam(outVelocity, ForceMode.VelocityChange);
            }

            m_state = EItemUseState.ITEM_STAT_USING;

            base.OnFire();

            return true;
        }
        // ウンコー
        else if( Input.GetButtonDown("Triangle") )
        {
            m_state = EItemUseState.ITEM_STAT_READY;
        }

        return false;
    }

    //---------------------------------------------------------------
    /*!
        @brief      ロケット発射処理その２
        @note       押してる間はパワーためる
        　　　　　　　離したら発射
    */
    //---------------------------------------------------------------
    public bool OnFire2()
    {
        if( m_state == EItemUseState.ITEM_STAT_USING )
        {
            if( m_reloadTime < ReloadTimer )
            {
                return false;
            }
        }

        Vector3 vForward = Vector3.zero;
        float angle = 0.0f;

        if( Input.GetButton("Circle") )
        {
            if( !m_bChargeStart )
            {
                PlayCharge();
                m_bChargeStart = true;
            }

            m_rocketPower += 0.01f;
            if( m_rocketPower >= 1.0f ) m_rocketPower = 1.0f;

#if UNITY_PS4
            PS4Input.PadSetVibration(0, 0, (int)(255 * m_rocketPower));
#endif  //
        }
        else if( Input.GetButtonUp("Circle") )
        {
            StopSE();
            m_bChargeStart = false;

            Quaternion camRot = scrCamera.hmdOrientation;
            float power = 0.0f;
            if( m_state == EItemUseState.ITEM_STAT_READY )
            {
                vForward = attachedObject.transform.forward;
                power = InitialRocketPower;
                angle = InitialRocketAngle;
            }
            else if( m_state == EItemUseState.ITEM_STAT_USING )
            {
                vForward = objScript.RigidBody.velocity;
                power = AddMaxPower;
                angle = AddMaxAngle * m_rocketPower;
            }

            Vector3 outVelocity;
            bool bLaunch = SetupRocketOrbit(out outVelocity, vForward, angle, power);

            if( bLaunch )
            {
                objScript.SetupBlowoffParam(outVelocity, ForceMode.VelocityChange);
                GenerateExplosion();
            }

            m_rocketPower = 0.0f;
            m_state = EItemUseState.ITEM_STAT_USING;

            base.OnFire();

#if UNITY_PS4
            PS4Input.PadSetVibration(0, 255, 0);
#endif  //

            return true;
        }
        // ウンコー
        else if( Input.GetButton("Triangle") )
        {
            m_state = EItemUseState.ITEM_STAT_READY;
        }
        else
        {
            m_rocketPower -= 0.01f;
            if( m_rocketPower < 0.0f ) m_rocketPower = 0.0f;
        }


        return false;
    }

    //---------------------------------------------------------------
    /*
        @brief      ロケット軌道設定
    */
    //---------------------------------------------------------------
    public bool SetupRocketOrbit(out Vector3 vOut, Vector3 vBase, float angle, float power)
    {
        if( !IsAttachedObject() )
        {
            vOut = Vector3.zero;
            return false;
        }

        Vector3 moveVector = Vector3.zero;
        float speed = vBase.magnitude;
        speed += power;

        Quaternion rot = Quaternion.AngleAxis(-angle, attachedObject.transform.right);

        Vector3 vWork = Vector3.Scale(vBase, new Vector3(0.0f, 0.0f, 1.0f));
        vWork = Vector3.Normalize(vWork);
        vWork *= speed;
        moveVector = rot * vWork;

        vOut = moveVector;
        return true;
    }

    public bool SetupRocketOrbit(out Vector3 vOut, Vector3 vBase, Quaternion rot, float power)
    {
        if( !IsAttachedObject() )
        {
            vOut = Vector3.zero;
            return false;
        }

        Vector3 moveVector = Vector3.zero;

        float speed = vBase.magnitude;
        speed += power;

        Vector3 vWork = Vector3.Scale(vBase, new Vector3(0.0f, 0.0f, 1.0f));
        vWork = Vector3.Normalize(vWork);
        vWork *= -speed;
        moveVector = rot * vWork;
        if( moveVector.z > 0.0f ) moveVector.z = -moveVector.z;

        vOut = moveVector;
        return true;
    }

    // 爆発エフェクト発生
    void GenerateExplosion()
    {
        if( !effectMngr ) return;

        effectMngr.PlayEffect("ShockFlame", transform.position);
        PlayExplosion();
    }

    //---------------------------------------------------------------
    /*
        @brief      最初のロケット噴射
    */
    //---------------------------------------------------------------
    public void FirstFire()
    {
        Vector3 vForward = attachedObject.transform.forward;
        float power = InitialRocketPower;
        float angle = InitialRocketAngle;

        Vector3 outVelocity;
        bool bLaunch = SetupRocketOrbit(out outVelocity, vForward, angle, power);

        if( bLaunch )
        {
            objScript.SetupBlowoffParam(outVelocity, ForceMode.VelocityChange);
            GenerateExplosion();
        }

        m_rocketPower = 0.0f;
        m_state = EItemUseState.ITEM_STAT_USING;

        base.OnFire();

#if UNITY_PS4
            PS4Input.PadSetVibration(0, 255, 0);
#endif  //
    }


    //---------------------------------------------------------------
    /*
        @brief      SE関係
    */
    //---------------------------------------------------------------
    private void Play(RocketSE type)
    {

        if( type == RocketSE.SE_EXPLOSION )
        {
            audioSource.clip = seExplosion;
        }
        else if( type == RocketSE.SE_AFTER_BURNER )
        {
            audioSource.clip = seAfterBurner;
        }
        else if( type == RocketSE.SE_CHARGE )
        {
            audioSource.clip = seCharge;
        }
        else
        {
            return;
        }
        audioSource.Play();         
    }

    private void StopSE()
    {
        audioSource.Stop();
    }

    /**
     *      爆発SE
     */
    public void PlayExplosion()
    {
        Play(RocketSE.SE_EXPLOSION);
    }

    /**
     *      アフターバーナーSE -> いらねぇ
     */
    public void PlayAfterBurner()
    {
        Play(RocketSE.SE_AFTER_BURNER);
    }

    /**
     *  ロケットチャージSE
     */
     public void PlayCharge()
    {
        Play(RocketSE.SE_CHARGE);
    }


    //-----------------------------------------------------------------
    void OnGUI()
    {
        /*
        GUI.TextField(new Rect(720.0f, 300.0f, 300, 180), "ReloadTime: " + m_reloadTime + "\n" +
                                                   "SpeedVec: " + objScript.RigidBody.velocity.ToString() + "\n" +
                                                   "Speed : " + objScript.RigidBody.velocity.magnitude + "\n" +  
                                                   "State: " + m_state + "\n" +  
                                                   "RocketPower:" + m_rocketPower + "\n" +
                                                   "Rot:" + scrCamera.hmdOrientation.ToString() );
        */
                                                    
    }
}
