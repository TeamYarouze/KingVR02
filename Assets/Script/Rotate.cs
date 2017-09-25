using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("z", 1, "easeType", "linear", "loopType","loop", "time" ,10));
	}

}

