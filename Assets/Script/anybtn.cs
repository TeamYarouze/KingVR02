using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anybtn : MonoBehaviour {

    public GameObject fuki;
    public GameObject modesel;

	// Use this for initialization
	void Start () {

        fuki = GameObject.Find("fukidashi");
//        modesel = transform.Find("ModeSelect").gameObject;
        modesel = GameObject.Find("ModeSelectobj");

        fuki.SetActive(true);
        modesel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_PS4

#else
        // 一旦封印します
        if (Input.GetButton("Triangle") || Input.GetButton("Cross") || Input.GetButton("Circle") || Input.GetButton("Square") || Input.GetButton("Triangle") )
        {
            //ボタンが押されたら吹き出しを消す。
            fuki.SetActive(false);
            modesel.SetActive(true);
        }
        
#endif  //
    }
}
