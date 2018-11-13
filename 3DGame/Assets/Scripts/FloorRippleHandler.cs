using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRippleHandler : MonoBehaviour {

    public RippleManager rippleManager;
    public Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Ripple ripple = rippleManager.getRipple();
        Vector4 rippleData;
        if (ripple.isActive) {
            rippleData = new Vector4(ripple.centerX, ripple.currRadius, ripple.centerZ, ripple.thickness);
        } else {
            rippleData = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        }
        renderer.material.SetVector("_Ripple", rippleData);
	}
}
