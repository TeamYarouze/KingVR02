//var target : Transform;
var distance = 10.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var xMax = 30;
var yMax = 30;

private var yMinLimit = 0;
private var yMaxLimit = 0;

private var xMinLimit = 0;
private var xMaxLimit = 0;

private var x = 0.0;
private var y = 0.0;


function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;
    
    xMinLimit = x - xMax;
    xMaxLimit = x + xMax;
    
    yMinLimit = y - yMax;
    yMaxLimit = y + yMax;

	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}

function LateUpdate () {
    if (Input.GetMouseButton(0) && Input.GetKey("left alt")) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        y += Input.GetAxis("Mouse Y") * ySpeed * 0.02;
 		
 		x = ClampAngle(x, xMinLimit, xMaxLimit);
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);

        
        transform.rotation = rotation;

    }
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}