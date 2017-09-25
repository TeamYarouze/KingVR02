//add button names here
var StrColor = Color(0.5,0.5,0.5);
var skinny : GUISkin;

var buttonWidth = 95.0; //modifies buttonWidth, for longer Strings

var guiEnabled = true;

private var screenwidth = Screen.width;
private var screenheight = Screen.height;

function LateUpdate (){

	screenwidth = Screen.width;
	screenheight = Screen.height;
}

function OnGUI () {
    if (Application.platform == RuntimePlatform.Android) {
        if (guiEnabled){
            GUI.skin = skinny;
            GUI.contentColor = StrColor;
            GUI.Label (Rect(10, screenheight - 20, 500, 30), "Android");
		
        }
    }
    else if (Application.platform == RuntimePlatform.IPhonePlayer) {
        if (guiEnabled){
            GUI.skin = skinny;
            GUI.contentColor = StrColor;
            GUI.Label (Rect(10, screenheight - 20, 500, 30), "iOS");
		
        }
    }
    else {
        if (guiEnabled){
            GUI.skin = skinny;
            GUI.contentColor = StrColor;
		    
            //GUI.HorizontalSlider (Rect(10, screenheight - 200, 500, 30), Input.acceleration.x,-10,10);
            //GUI.HorizontalSlider (Rect(10, screenheight - 180, 500, 30), Input.acceleration.y,-10,10);

            GUI.Label (Rect(10, screenheight - 140, 500, 30), "ALT+LMB:LightRotate");
            GUI.Label (Rect(10, screenheight - 120, 500, 30), "X:PoseChange");
            GUI.Label (Rect(10, screenheight - 100, 500, 30), "LMB:CameraRotate");
            GUI.Label (Rect(10, screenheight - 80, 500, 30), "RMB:CameraPan");
            GUI.Label (Rect(10, screenheight - 60, 500, 30), "Wheel:CameraZoom");
            GUI.Label (Rect(10, screenheight - 40, 500, 30), "WASDQE:WalkthroughMove");
            GUI.Label (Rect(10, screenheight - 20, 500, 30), "R:CameraReset");
		
        }
    }
}

//@script ExecuteInEditMode();