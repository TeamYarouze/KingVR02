using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBord : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        this.transform.LookAt(p);

    }
}
