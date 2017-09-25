﻿#pragma strict

private var facemoof:SkinnedMeshRenderer;


var camObj : Transform;
var faceObj : GameObject;
var constObj : Transform;

var moofVal = 20;

private var hAngleR = 0.0;
private var hAngleL = 0.0;
private var vAngleU = 0.0;
private var vAngleD = 0.0;
private var camPos = Vector3(0.0,0.0,0.0);
private var constangles = Vector3(0.0,0.0,0.0);
private var hangles = Vector3(0.0,0.0,180);

private var hAngle = 0.0;
private var vAngle = 0.0;

private var constPos = Vector3(0.0,0.0,0.0);
private var constRot = Quaternion(0.0, 0.0, 0.0,0.0);

@script AddComponentMenu("Script/camAngle")

function Start(){
	facemoof = faceObj.GetComponent(SkinnedMeshRenderer);
}
function Update () {

	camPos = constObj.transform.InverseTransformPoint(camObj.transform.position);
	var hangle = Mathf.Atan2(Mathf.Abs(camPos.x),Mathf.Max(Mathf.Abs(camPos.z),0.2))* Mathf.Rad2Deg;
	var vangle = Mathf.Atan2(Mathf.Abs(camPos.y),Mathf.Max(Mathf.Abs(camPos.z),0.2))* Mathf.Rad2Deg;

	hAngleR = Mathf.Max(hangle - vangle,0.0);
	vAngleU = Mathf.Max(vangle - hangle,0.0);

	hAngleR = (hAngleR * hAngleR + 5) / moofVal;
	vAngleU = (vAngleU * vAngleU + 5) / moofVal;
	
	hAngleL = hAngleR;
	vAngleD = vAngleU;
	
	if(camPos.x < 0){
		hAngleR *= -1;
	}
	else{
		hAngleL *= -1;
	}
	
	if(camPos.y < 0){
		vAngleU *= -1;
	}
	else{
		vAngleD *= -1;
	}
	
	facemoof.SetBlendShapeWeight(0,hAngleR);
	facemoof.SetBlendShapeWeight(1,hAngleL);
	facemoof.SetBlendShapeWeight(2,vAngleU);
	facemoof.SetBlendShapeWeight(3,vAngleD);
}
