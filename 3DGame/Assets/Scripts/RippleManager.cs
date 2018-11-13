using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple {
    public float centerX;
    public float centerZ;
    public float currRadius;
    public float maxRadius;
    public float thickness;
    public bool isActive;

    public Ripple() {
        centerX = 0f;
        centerZ = 0f;
        currRadius = 0f;
        maxRadius = 0f;
        thickness = 0f;
        isActive = false;
    }
}

public class RippleManager : MonoBehaviour {

    public int maxNumRipples = 1;
    public float rippleSpeed = 1f;

    List<Ripple> ripples;
    int oldestRippleIndex;

	// Use this for initialization
	void Start () {
        ripples = new List<Ripple>();
        for (int i = 0; i < maxNumRipples; i++){
            ripples.Add(new Ripple());
        }
        oldestRippleIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        DebugRipple(); //TODO: remove this

        foreach (Ripple ripple in ripples) {
            if (ripple.isActive) {
                ripple.currRadius += Time.deltaTime * rippleSpeed;
                if (ripple.currRadius > ripple.maxRadius) {
                    ripple.isActive = false;
                }
            }
        }
	}

    public void CreateRipple (float centerX, float centerZ, float maxRadius, float thickness) {
        Ripple newRipple = ripples[oldestRippleIndex];
        newRipple.centerX = centerX;
        newRipple.centerZ = centerZ;
        newRipple.currRadius = 0f;
        newRipple.maxRadius = maxRadius;
        newRipple.thickness = thickness;
        newRipple.isActive = true;

        oldestRippleIndex++;
        if (oldestRippleIndex >= maxNumRipples) {
            oldestRippleIndex = 0;
        }
    }

    public Ripple getRipple() {
        return ripples[0];
    }

    void DebugRipple() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CreateRipple(transform.position.x, transform.position.y, 100f, 0.1f);
        }
        //Debug.Log("ripple[0] currRadius: " + ripples[0].currRadius);
    }
}
