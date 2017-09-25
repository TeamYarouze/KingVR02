var target : Transform;

var zoomSpeed = 5;
var panSpeed = 1;
var moveSpeed = 0.1;

var distance = 3.0;
var distanceMin = 0.5;
var distanceMax = 20.0;
var distanceReset = 0;
var hight = 0;

var orbit_xSpeed = 17.0;
var orbit_ySpeed = 17.0;

var orbit_yMin = -90;
var orbit_yMax = 90;

var autoRotSpeed = 0.5;

var firstTargetPos = Vector3(0.0, 0.0, 0.0);
var firstRot = Quaternion(0.0, 0.0, 0.0,0.0);

private var autoRotate = false;
private var oldTime = 0.0;
private var x = 0.0;
private var y = 0.0;

@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {

	target.transform.position = firstTargetPos;
	transform.rotation = firstRot;

    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

    distanceReset = distance;

	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}

function LateUpdate (){
	
	//keyEvent
	if (Input.GetKey("w")){
		target.transform.Translate(Vector3.forward * moveSpeed);
	}
	if (Input.GetKey("a")){
		target.transform.Translate(Vector3.left * moveSpeed);
	}
	if (Input.GetKey("s")){
		target.transform.Translate(Vector3.back * moveSpeed);
	}
	if (Input.GetKey("d")){
		target.transform.Translate(Vector3.right * moveSpeed);
	}
	if (Input.GetKey("q")){
		target.transform.Translate(Vector3.up * moveSpeed);
	}
	if (Input.GetKey("e")){
		target.transform.Translate(Vector3.down * moveSpeed);
	}
	if(Input.GetKey("t")){
		if((oldTime + 0.5) < Time.time){
			autoRotate = !autoRotate;
			oldTime = Time.time;
		}
	}
	if (Input.GetKey("r")){
		target.transform.position = firstTargetPos;
		transform.rotation = firstRot;
		
		var angles = transform.eulerAngles;
   		x = angles.y;
   		y = angles.x;

   		distanceReset = distance;
	}
	if (Input.GetKey("p")){
		print("TargetPos_X" + target.transform.position[0] + " Y_" + target.transform.position[1] + " Z_" + target.transform.position[2] +
			 "  Rot_X" + transform.rotation[0] + " Y_" + transform.rotation[1] + " Z_" + transform.rotation[2] + " W_" + transform.rotation[3] +
			 " Distance_" + distance);
	}
	
	//zoom
	if (Input.GetAxis("Mouse ScrollWheel")) {
		distance += (-Input.GetAxis("Mouse ScrollWheel")) * zoomSpeed;
		distance = Mathf.Clamp (distance, distanceMin, distanceMax);
    }
    
    //pan
    target.transform.rotation = transform.rotation;
	
	if( Input.GetMouseButton(2)){
		var panVal = panSpeed * (distance / 20);
		if(Input.GetAxis("Mouse Y") != 0){
			target.transform.Translate(-Vector3.up * Input.GetAxis("Mouse Y") * panVal);// * Time.deltaTime);
			transform.Translate(-Vector3.up * Input.GetAxis("Mouse Y") * panVal);// * Time.deltaTime);
		}
		if(Input.GetAxis("Mouse X") != 0){
			target.transform.Translate(-Vector3.right * Input.GetAxis("Mouse X") * panVal);// * Time.deltaTime);
			transform.Translate(-Vector3.right * Input.GetAxis("Mouse X") * panVal);// * Time.deltaTime);
		}
	}
	//orbit
    if (target && Input.GetMouseButton(0) && !Input.GetKey("left alt")) {
       
        x += Input.GetAxis("Mouse X") * orbit_xSpeed;
        y -= Input.GetAxis("Mouse Y") * orbit_ySpeed;
 		
 		
    }else if(autoRotate){
    	x += autoRotSpeed * Time.deltaTime;
    }
    y = ClampAngle(y, orbit_yMin, orbit_yMax);
 		       
    var rotation = Quaternion.Euler(y, x, 0);
    var position = rotation * Vector3(0.0, hight, -distance) + target.position;
   	transform.rotation = rotation;
   	transform.position = position;
    
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}