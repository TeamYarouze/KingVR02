using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    private int EFFECT_OBJ_MAX = 64;
    private int NOT_LOOP_EFFECT_IDX = 65;

    private GameObject[] m_effectArray;

    public void Awake()
    {

        m_effectArray = new GameObject[EFFECT_OBJ_MAX];
        for(int idx = 0; idx < EFFECT_OBJ_MAX; idx++)
        {
            m_effectArray[idx] = null;
        }
    }

	void Start () {

        
	}
	
	void Update ()
    {
	}

    private int GetEmptyIndex()
    {
        for(int idx = 0; idx < EFFECT_OBJ_MAX; idx++)
        {
            if( m_effectArray[idx] == null )
            {
                return idx;
            }
        }
        return -1;
    }

    //---------------------------------------------------------------
    /*
        @brief      エフェクトを取得する
        @param[in]  int index インデックス
    */
    //---------------------------------------------------------------
    public GameObject GetEffect(int index)
    {        
        return transform.GetChild(index).gameObject;
    }

    //---------------------------------------------------------------
    /*
        @brief      エフェクトを取得する
        @param[in]  string name エフェクト名
    */
    //---------------------------------------------------------------
    public GameObject GetEffect(string name)
    {
        return transform.FindChild(name).gameObject;
    }

    
    private int _PlayEffect(GameObject effect, Vector3 position, int arrayIdx)
    {
        if( !effect ) return -1;
        if( arrayIdx < 0 ) return -1;

        int ret = NOT_LOOP_EFFECT_IDX;
        GameObject workObj = Instantiate<GameObject>(effect, position, effect.transform.rotation);
        if( workObj )
        {          
            workObj.GetComponent<ParticleSystemBase>().Play();
            // ループするエフェクトは管理する
            if( workObj.GetComponent<ParticleSystemBase>().IsLoop() )
            {
                m_effectArray[arrayIdx] = workObj;
                ret = arrayIdx;
            }
        }

        return ret;
    }

    //---------------------------------------------------------------
    /*
        @brief      エフェクトを再生する
    */
    //---------------------------------------------------------------
    public int PlayEffect(int index, Vector3 position)
    {
        GameObject effectObj = GetEffect(index);
        if( effectObj == null ) return -1;

        int idx = GetEmptyIndex();
        if( idx == -1 ) return -1;      // もう再生数max

        return _PlayEffect(effectObj, position, idx);
    }

    public int PlayEffect(string name, Vector3 position)
    {
        GameObject effectObj = GetEffect(name);
        if( effectObj == null ) return -1;

        int idx = GetEmptyIndex();
        if( idx == -1 ) return -1;      // 再生数max

        return _PlayEffect(effectObj, position, idx);
    }

    //---------------------------------------------------------------
    /*
        @brief      エフェクトを停止する
    */
    //---------------------------------------------------------------
    public void StopEffect(int idx)
    {
        if( idx >= EFFECT_OBJ_MAX ) return;
        GameObject effect = m_effectArray[idx];

        if( !effect ) return;

        effect.GetComponent<ParticleSystemBase>().Stop();
        m_effectArray[idx] = null;
    }


    //---------------------------------------------------------------
    /*
        @brief      エフェクトが再生中かどうか
    */
    //---------------------------------------------------------------
    public bool IsEffectPlay(int idx)
    {
        if( idx >= EFFECT_OBJ_MAX ) return false;
        GameObject effect = m_effectArray[idx];
        if( effect == null ) return false;

        return effect.GetComponent<ParticleSystemBase>().IsPlay();
    }




}
