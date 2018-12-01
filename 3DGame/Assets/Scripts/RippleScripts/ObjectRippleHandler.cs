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

    protected RippleManager _rippleManager;
    protected Renderer _renderer;
    protected float currVibrationTime = 0.0f;
    protected bool isVibrating = false;

    LightRippleHandler _lightRippleHandler;


    // Use this for initialization
    void Start()
    {
        // Initialize all shader variables
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetFloat("_VibrationProgress", -1.0f);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        _renderer.material.SetFloat("_MaxMeshY", mesh.bounds.max.y);

        _renderer.material.SetFloat("_WidthOfVibration", vibrationWidth);
        _renderer.material.SetFloat("_PeriodOfVibration", vibrationPeriod);
        _renderer.material.SetFloat("_AmplitudeOfVibration", vibrationAmplitude);

        _renderer.material.SetColor("_BaseColor", baseColor);
        _renderer.material.SetFloat("_ColorOffset", colorOffset);

        _rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();

        // If object has light column, initialize that too
        _lightRippleHandler = GetComponentInChildren<LightRippleHandler>();
        if (_lightRippleHandler != null)
        {
            _lightRippleHandler.baseColor = baseColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunction();
    }

    public void Visuals()
    {
        // Has a ripple crossed the object?
        if (isVibrating) {
            if (currVibrationTime > maxVibrationTime) {
                isVibrating = false;
                _renderer.material.SetFloat("_VibrationProgress", -1.0f);
                // turn off light column if it has one
                if (_lightRippleHandler != null)
                {
                    _lightRippleHandler.TurnOffLight();
                }
            }
            else {
                currVibrationTime += Time.deltaTime;
                _renderer.material.SetFloat("_VibrationProgress",
                                           Mathf.Lerp(
                                               minVibrationHeight,
                                               maxVibrationHeight,
                                               (currVibrationTime / maxVibrationTime)));
                // turn on light column if it has one
                if (_lightRippleHandler != null)
                {
                    _lightRippleHandler.TurnOnLight();
                }
            }
        }
    }

    public virtual void UpdateFunction()
    {
        List<Ripple> ripples = _rippleManager.getRipples();
        foreach (Ripple ripple in ripples) {
            // check if any ripple touches this object
            if (ripple.isActive
                && Mathf.Abs(calculateDistance(ripple) - ripple.currRadius) < (ripple.thickness + modelRadius) 
                && !isVibrating) {
                isVibrating = true;
                currVibrationTime = 0.0f;
            }
        }

        Visuals();
    }

    // Calculate distance from center of ripple to this object
    protected float calculateDistance(Ripple ripple) {
        return Mathf.Sqrt(
            Mathf.Pow((ripple.centerX - transform.position.x), 2) + 
            Mathf.Pow((ripple.centerZ - transform.position.z), 2));
    }
}
