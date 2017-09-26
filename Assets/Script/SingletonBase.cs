using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if( _instance == null )
            {

                _instance = (T)FindObjectOfType(typeof(T));
                if( _instance == null )
                {
                    Debug.LogError("Instance Of " + typeof(T) + " is Need In the Scene !!!!\n");
                }
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if( this == Instance )
        {
            return true;
        }

        Destroy(this);
        return false;
    }

    public void OnDestroy()
    {
        Debug.Log("---- Destory GameObject :" + typeof(T));
        Destroy(gameObject);
    }
}
