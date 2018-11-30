﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRippleHandler : MonoBehaviour {

    RippleManager rippleManager;
    int maxNumRipples;
    Renderer renderer;
    Vector4[] rippleData;

	// Use this for initialization
	void Start () {
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();

        maxNumRipples = rippleManager.maxNumRipples;

        renderer = GetComponent<Renderer>();

        rippleData = new Vector4[maxNumRipples];
        for (int i = 0; i < maxNumRipples; i++) {
            rippleData[i] = new Vector4(0f, 0f, 0f, 0f);
        }
        renderer.material.SetInt("_NumRipples", maxNumRipples);
    }
	
	// Update is called once per frame
	void Update () {
        List<Ripple> ripples = rippleManager.getRipples();
        // Draws every ripple on floor
        for (int i = 0; i < maxNumRipples; i++)
        {
            Ripple ripple = ripples[i];
            //Debug.Log(ripple.sourceTag);
            if (ripple.isActive)
            {
                rippleData[i].x = ripple.centerX;
                rippleData[i].z = ripple.centerZ;
                rippleData[i].y = ripple.currRadius;
                rippleData[i].w = ripple.thickness;
            } else {
                rippleData[i].x = 0f;
                rippleData[i].z = 0f;
                rippleData[i].y = 0f;
                rippleData[i].w = 0f;
            }
        }
        renderer.material.SetVectorArray("_Ripples", rippleData);

	}
}
