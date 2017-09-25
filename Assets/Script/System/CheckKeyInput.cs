using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckKeyInput : MonoBehaviour {

        public enum BUTTON_ID
    {
        BTN_INVALID  = 0,

        BTN_TRIANGLE = (1 << 0),
        BTN_CIRCLE   = (1 << 1),
        BTN_CROSS    = (1 << 2),
        BTN_SQUARE   = (1 << 3),

        BTN_L1       = (1 << 4),
        BTN_L2       = (1 << 5),
        BTN_R1       = (1 << 6),
        BTN_R2       = (1 << 7),

    };


    private uint m_btnPush;
    private uint m_btnTrigger;
    private uint m_btnRelease;
    private uint m_btnPrev;

    // Use this for initialization
    void Start () {
		
        m_btnPush = 0;
        m_btnTrigger = 0;
        m_btnRelease = 0;
        m_btnPrev = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
        uint bit = 0;

        bit = (uint)StringToButtonID("Triangle");
        if( Input.GetButton("Triangle") )
        {

            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnRelease &= ~bit;
            m_btnPush |= bit;
        }
        else
        {
            m_btnRelease |= (m_btnPrev & bit);
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }
        
        bit = (uint)StringToButtonID("Circle");
        if( Input.GetButton("Circle") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("Cross");
        if( Input.GetButton("Cross") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("Square");
        if( Input.GetButton("Square") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("L1");
        if( Input.GetButton("L1") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("L2");
        if( Input.GetButton("L2") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("R1");
        if( Input.GetButton("R1") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        bit = (uint)StringToButtonID("R2");
        if( Input.GetButton("R2") )
        {
            m_btnTrigger |= (~(m_btnPrev & bit) & bit);
            m_btnPush |= bit;
        }
        else
        {
            m_btnPush &= ~bit;
            m_btnTrigger &= ~bit;
        }

        m_btnPrev = m_btnPush;
	}

    public BUTTON_ID StringToButtonID(string button)
    {
        if( button == "Triangle" )       return BUTTON_ID.BTN_TRIANGLE;
        else if( button == "Circle" )    return BUTTON_ID.BTN_CIRCLE;
        else if( button == "Cross" )     return BUTTON_ID.BTN_CROSS;
        else if( button == "Square" )    return BUTTON_ID.BTN_SQUARE;
        else if( button == "L1")         return BUTTON_ID.BTN_L1;
        else if( button == "L2")         return BUTTON_ID.BTN_L2;
        else if( button == "R1")         return BUTTON_ID.BTN_R1;
        else if( button == "R2")         return BUTTON_ID.BTN_R2;

        return BUTTON_ID.BTN_INVALID;
    }

    /*
    public void OnGUI()
    {
        GUI.TextField(new Rect(120.0f, 400.0f, 300, 52), "Push:" + Convert.ToString(m_btnPush, 2) + "\n" + 
                                                         "Trigger:" + Convert.ToString(m_btnTrigger, 2) + "\n" +
                                                         "Release:" + Convert.ToString(m_btnRelease, 2) );
    }
    */
}
