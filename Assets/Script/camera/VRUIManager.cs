using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIManager : MonoBehaviour {

    private GameObject ui_ReadyGoRoot;
    private GameObject ui_GoalRoot;

    public CountDown ScrCountDown
    {
        get
        {
            return ui_ReadyGoRoot.transform.GetChild(0).gameObject.GetComponent<CountDown>();
        }
    }

    public GoalUI ScrGoalUI
    {
        get
        {
            return ui_GoalRoot.transform.GetChild(0).gameObject.GetComponent<GoalUI>();
        }
    }

	// Use this for initialization
	void Start () {
		
        ui_ReadyGoRoot = transform.FindChild("VRUI_ReadyGoRoot").gameObject;
        ui_GoalRoot = transform.FindChild("VRUI_GoalRoot").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
