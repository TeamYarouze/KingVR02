using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_cube : MonoBehaviour {

    static int MOVE_DIV = 8;
    public float gravity = 0.0f;
    public float speed = 1.0f;
    public float jumpSpeed = 0.0f;

    private GameObject freeCamera;

	// Use this for initialization
	void Start () {
        freeCamera = null;
		freeCamera = GameObject.Find("FreeCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetButtonDown("Fire1") )
        {
            jumpSpeed = 5.0f;
        }
        
        if( jumpSpeed != 0.0f )
        {
            jumpSpeed -= 0.1f;
        }        
    }

    void FixedUpdate()
    {

        float lh = Input.GetAxis("Horizontal");
        float lv = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 direction = forward * lv * speed + right * lh *speed;

        Vector3 pos = transform.position;

        if( (int)gravity != 0 )
        {
            for(int i = 0; i < MOVE_DIV; i++)
            {
                float div_gravity = gravity / MOVE_DIV;
                pos.y -= div_gravity;
                transform.position = pos;
            }
        }

        pos.y += jumpSpeed;
        pos += direction;
        transform.position = pos;
    }

    void LateUpdate()
    {
//        UpdateRotationByCamera();

        string infoStr = "";
        infoStr += "cube forward: " + transform.forward.ToString() + "\n";
        infoStr += "cube right  : " + transform.right.ToString() + "\n";
        infoStr += "Jump :" + jumpSpeed + "\n";
        GameObject.Find("Text").GetComponent<scr_GUIText>().AddText(infoStr);
    }

    // FreeCameraの角度を引っ張ってくるが、お互いに依存関係を作ってしまうのがマズいよな
    // テストだからいいや
    void UpdateRotationByCamera()
    {
        if( !freeCamera ) return;

        float yaw = freeCamera.GetComponent<scr_Camera>().Angle_Yaw;
        
        Quaternion rot = Quaternion.Euler(0.0f, yaw, 0.0f);
        transform.rotation = rot;
    }

    void OnCollisionEnter(Collision collision)
    {
        gravity = 0.0f;
        jumpSpeed = 0.0f;
    }
}
