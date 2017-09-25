using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeight : ItemBase {

    [SerializeField]
    private float WeightInitialAngle = -60.0f;
    [SerializeField]
    private float WeightPower = -1.0f;
    [SerializeField]
    private float WeightGravity = 1.0f;

    private bool bButtonOn = false;

    private UpdateStage stageUpdater;
    private AudioSource audioSource;

	// Use this for initialization
	new void Start () {
		base.Start();

        stageUpdater = GameObject.Find("SceneUpdater").GetComponent<UpdateStage>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	new void Update () {

        if( !IsAttachedObject() )
        {
            return;
        }

        if( stageUpdater.CurrentState != UpdateStage.StageState.STATE_INGAME )
        {
            return;
        }

        UpdateReloadTime();
		
        if( !OnFire() )
        {
            m_state = EItemUseState.ITEM_STAT_READY;
        }
	}

    public void FixedUpdate()
    {
        if( !IsAttachedObject() )
        {
            return;
        }

        FixedOnFire();
    }

    //---------------------------------------------------------------
    /*
        @brief      発射処理
    */
    //---------------------------------------------------------------
    new public bool OnFire()
    {
        bool bLaunch = false;

        Vector3 outVelocity = Vector3.zero;
        Vector3 vForward;

        if( Input.GetButton("Triangle") )
        {
            bButtonOn = true;
            
            bLaunch = true;           
        }
        else
        {
            bButtonOn = false;
        }

        if( bLaunch )
        {
            base.OnFire();
        }


        return bLaunch;
    }

    public void StartWeight(out Vector3 vOut, Vector3 vBase, float angle)
    {
        if( !IsAttachedObject() )
        {
            vOut = Vector3.zero;
            return;
        }

        vBase = Vector3.Normalize(vBase);

        Vector3 moveVector = Vector3.zero;
        Quaternion rot = Quaternion.AngleAxis(-angle, attachedObject.transform.right);

        Vector3 vWork = Vector3.Scale(vBase, new Vector3(0.0f, 0.0f, 1.0f));
        vWork = Vector3.Normalize(vWork);
        vWork *= objScript.RigidBody.velocity.magnitude;
//        vBase = Vector3.Normalize(vBase);
//        moveVector = rot * vBase;
//        moveVector *= objScript.RigidBody.velocity.magnitude;

        moveVector = rot * vWork;

        vOut = moveVector;
    }

    public void SetupWeightOrbit(out Vector3 vOut, Vector3 vBase, float power, float gravity)
    {
        if( !IsAttachedObject() )
        {
            vOut = Vector3.zero;
            return;
        }

        Vector3 moveVector = Vector3.zero;

        vBase = Vector3.Normalize(vBase);
        float magnitude = objScript.RigidBody.velocity.magnitude - power;

        moveVector = vBase * magnitude;
        moveVector.y -= gravity;

        vOut = moveVector;

        return;

    }

    private void CheckSpeedLimit(Vector3 inVelocity, out Vector3 outVelocity)
    {
        Vector3 vSpeed = inVelocity;
        float speed = inVelocity.magnitude;

        Vector3 unitVelocity = Vector3.Normalize(vSpeed);

        if( speed > 60.0f )
        {
            speed = 60.0f;
            vSpeed = unitVelocity * speed;
        }

        outVelocity = vSpeed;
    }

    private void FixedOnFire()
    {
        Vector3 outVelocity = Vector3.zero;
        Vector3 vForward;

        if( bButtonOn )
        {
            if( m_state == EItemUseState.ITEM_STAT_READY )
            {
                m_Angle = WeightInitialAngle;
                vForward = objScript.RigidBody.velocity;

                StartWeight(out outVelocity, vForward, m_Angle);
                PlaySE();

                m_state = EItemUseState.ITEM_STAT_USING;
            }
            else if( m_state == EItemUseState.ITEM_STAT_USING )
            {
                m_Power = WeightPower;
                m_Gravity = WeightGravity;

                vForward = objScript.RigidBody.velocity;

                SetupWeightOrbit(out outVelocity, vForward, m_Power, m_Gravity);

                m_state = EItemUseState.ITEM_STAT_USING;
            }

//            CheckSpeedLimit(outVelocity, out outVelocity);
            objScript.SetupBlowoffParam(outVelocity, ForceMode.VelocityChange);

        }
    }

    private void PlaySE()
    {
        audioSource.Play();
    }


    void OnGUI()
    {
        /*
        GUI.TextField(new Rect(720.0f, 480.0f, 300, 80), "ReloadTime: " + m_reloadTime + "\n" +
                                                   "SpeedVec: " + objScript.RigidBody.velocity.ToString() + "\n" +
                                                   "Speed : " + objScript.RigidBody.velocity.magnitude + "\n" +  
                                                   "State: " + m_state );
        */
                                                    
    }

}
