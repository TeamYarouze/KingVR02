#pragma strict

var anim : Animator;
var flag = 0;
private var oldTime = 0.0;

function Start () {
    anim = GetComponent(Animator);
}

function Update () {
    if (Input.GetKey("x")){
        if((oldTime + 0.5) < Time.time){
            switch(flag%3){
                case 0 : anim.SetTrigger("pose02");break;
                case 1 : anim.SetTrigger("pose03");break;
                case 2 : anim.SetTrigger("pose01");break;
            }
            flag++;
            oldTime = Time.time;
        }
    }
}