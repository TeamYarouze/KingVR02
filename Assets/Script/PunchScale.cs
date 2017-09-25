using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        iTween.PunchScale(this.gameObject, iTween.Hash("x", 1, "y", 1, "time", 2.0f));
    }
}
