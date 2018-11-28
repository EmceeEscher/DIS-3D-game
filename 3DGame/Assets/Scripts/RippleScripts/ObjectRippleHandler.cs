using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectRippleHandler : MonoBehaviour {

    [Tooltip("Amount of time object will vibrate after being touched by a ripple.")]
    public float maxVibrationTime = 5.0f;

    [Tooltip("Relative point on object where center of vibration will start (0 is the base).")]
    public float minVibrationHeight = -0.2f;

    [Tooltip("Relative point on object where center of vibration will end (1 is the top).")]
    public float maxVibrationHeight = 1.2f;

    [Tooltip("Vertical width of vibration on object.")]
    public float vibrationWidth = 0.5f;

    [Tooltip("Period coeffecient of sine curve of vibration (actual period is 2pi/this value).")]
    public float vibrationPeriod = 25.0f;

    [Tooltip("Amplitude of sine curve of vibration.")]
    public float vibrationAmplitude = 3.0f;

    [Tooltip("Distance from center where ripple will activate vibration.")]
    public float modelRadius = 0.0f;

    [Tooltip("Base color given to shader.")]
    public Color baseColor;

    [Tooltip("How different the colors is at the center of the ripple vs. the edges. (Must be between 0 and 1.)")]
    public float colorOffset = 0.5f;

    RippleManager rippleManager;

    protected Renderer renderer;
    protected float currVibrationTime = 0.0f;
    protected bool isVibrating = false;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_VibrationProgress", -1.0f); // TODO will shader ignore these vars or break?

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        renderer.material.SetFloat("_MaxMeshY", mesh.bounds.max.y); // TODO more customizable for different models

        renderer.material.SetFloat("_WidthOfVibration", vibrationWidth);
        renderer.material.SetFloat("_PeriodOfVibration", vibrationPeriod);
        renderer.material.SetFloat("_AmplitudeOfVibration", vibrationAmplitude);

        renderer.material.SetColor("_BaseColor", baseColor);
        renderer.material.SetFloat("_ColorOffset", colorOffset);

        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
    }

    public virtual void Visuals()
    {
        // Has a ripple crossed the object?
        if (isVibrating) {
            if (currVibrationTime > maxVibrationTime) {
                isVibrating = false;
                renderer.material.SetFloat("_VibrationProgress", -1.0f);
            }
            else {
                currVibrationTime += Time.deltaTime;
                renderer.material.SetFloat("_VibrationProgress",
                                           Mathf.Lerp(
                                               minVibrationHeight,
                                               maxVibrationHeight,
                                               (currVibrationTime / maxVibrationTime)));
            }
        }
    }

    public virtual void UpdateFunction()
    {
        List<Ripple> ripples = rippleManager.getRipples();
        foreach (Ripple ripple in ripples) {
            if (ripple.isActive
                && Mathf.Abs(calculateDistance(ripple) - ripple.currRadius) < (ripple.thickness + modelRadius) 
                && !isVibrating) {
                isVibrating = true;
                currVibrationTime = 0.0f;
            }
        }

        Visuals();
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateFunction();
    }

    float calculateDistance(Ripple ripple) {
        return Mathf.Sqrt(
            Mathf.Pow((ripple.centerX - transform.position.x), 2) + 
            Mathf.Pow((ripple.centerZ - transform.position.z), 2));
    }
}
