using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctionality : MonoBehaviour {

    public float stepFrequency = 2.0f;
    public float distanceThreshold = 0.5f;
    public float rippleRange = 20.0f;
    public float rippleThickness = 1.0f;

    [HideInInspector]
    public bool isMoving = false;

    RippleManager rippleManager;
    float lastStepTime;

	// Use this for initialization
	void Start () {
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
        lastStepTime = stepFrequency;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving && lastStepTime >= stepFrequency) {
            lastStepTime = 0.0f;
            rippleManager.CreateRipple(transform.position.x, transform.position.z, rippleRange, rippleThickness);
        }
        lastStepTime += Time.deltaTime;
	}
}
