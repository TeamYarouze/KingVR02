using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DBGObj : MonoBehaviour {

    private GameObject targetObj;

	// Use this for initialization
	void Start () {
		targetObj = GameObject.Find("unitychan");
	}
	
	// Update is called once per frame
	void Update () {
        /*
        Vector3 pos = targetObj.transform.position;

        pos.y += 8.0f;
		transform.position = pos;
        transform.rotation = Quaternion.LookRotation(targetObj.GetComponent<CharAction>().VectorToMove);
        */

//        transform.localScale = new Vector3(1.0f, targetObj.GetComponent<CharAction>().BlowoffPower, 1.0f);
	}

    void LateUpdate()
    {
        string infoStr = "";
        infoStr += "UnityChan" + targetObj.transform.position.ToString() + "\n";
        GameObject.Find("Text").GetComponent<scr_GUIText>().AddText(infoStr);
    }
}
