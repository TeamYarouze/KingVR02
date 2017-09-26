using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    protected GameObject attachedObject;    // 自分がアタッチしているOBJ
    protected CharAction objScript;         // OBJのスクリプトコンポーネント

    protected float m_Angle;
    protected float m_Power;
    protected float m_Gravity;
    public float Gravity
    {
        get { return m_Gravity; }
    }
    
    private int m_ID;
    public int ID
    {
        set { m_ID = value; }
        get { return m_ID; }
    }

    protected GameDefine.ITEM_TYPE m_type;
    public GameDefine.ITEM_TYPE Type
    {
        set { m_type = value; }
        get { return m_type; }
    }

    protected int m_reloadTime;
    public int ReloadTime
    {
        get { return m_reloadTime; }
    }

    public enum EItemUseState
    {
        ITEM_STAT_READY,    // 使用前
        ITEM_STAT_USING,    // 使用中
        ITEM_STAT_DONE,     // 使用済み
        ITEM_STAT_MAX
    };
    protected EItemUseState m_state;
    public EItemUseState State
    {
        get { return m_state; }
    }

    protected int m_useType;
    public int UseType
    {
        get { return m_useType; }
    }

    private float elapsedTime;

    void Awake()
    {
        attachedObject = null;
        objScript = null;
        m_state = EItemUseState.ITEM_STAT_READY;
        
        m_type = GameDefine.ITEM_TYPE.ITEM_TYPE_INVALID;
        m_useType = 0;
        m_reloadTime = 0;
        elapsedTime = 0.0f;
    }

	// Use this for initialization
	public void Start () {
        /*
		attachedObject = null;
        objScript = null;
        m_state = EItemUseState.ITEM_STAT_READY;
        
        m_type = GameDefine.ITEM_TYPE.ITEM_TYPE_INVALID;
        m_useType = 0;
        m_reloadTime = 0;
        elapsedTime = 0.0f;
        */
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

    //---------------------------------------------------------------
    /*
        @brief      発射
    */
    //---------------------------------------------------------------
    public bool OnFire()
    {
        elapsedTime = 0.0f;
        m_reloadTime = 0;
        return false;
    }


    //---------------------------------------------------------------
    /*
        @brief      リロードタイムの更新
    */
    //---------------------------------------------------------------
    public void UpdateReloadTime()
    {
        if( m_state != EItemUseState.ITEM_STAT_USING )
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if( elapsedTime >= 1.0f )
        {
            elapsedTime = 0.0f;
            m_reloadTime++;
        }
    }

    //---------------------------------------------------------------
    /*
        @brief      リセット
    */
    //---------------------------------------------------------------
    public void ResetItem()
    {
        elapsedTime = 0.0f;
        m_reloadTime = 0;   

        m_state = EItemUseState.ITEM_STAT_READY;
    }

    //---------------------------------------------------------------
    /*
        @brief      オブジェクトへのアタッチ
    */
    //---------------------------------------------------------------
    public void AttachObject(GameObject target)
    {
        if( attachedObject || !target )
        {
            return;
        }
        attachedObject = target;
        objScript = attachedObject.GetComponent<CharAction>();

        transform.parent = target.transform;
    }

    //---------------------------------------------------------------
    /*
        @brief      オブジェクトから外す
    */
    //---------------------------------------------------------------
    public void RemoveObject()
    {
        attachedObject = null;
        objScript = null;

        transform.parent = null;
    }

    //---------------------------------------------------------------
    /*
        @brief      オブジェクトにアタッチされているのかどうか？
    */
    //---------------------------------------------------------------
    public bool IsAttachedObject()
    {
        if( attachedObject ) return true;
        return false;
    }



}
