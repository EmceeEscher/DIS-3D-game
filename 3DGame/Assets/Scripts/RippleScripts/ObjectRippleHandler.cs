using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectRippleHandler : MonoBehaviour {

    public float maxVibrationTime = 5.0f;

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

        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
    }

    public virtual void Visuals()
    {
        if (isVibrating) {
            if (currVibrationTime > maxVibrationTime) {
                isVibrating = false;
                renderer.material.SetFloat("_VibrationProgress", -1.0f);
            }
            else {
                currVibrationTime += Time.deltaTime;
                renderer.material.SetFloat("_VibrationProgress", (currVibrationTime / maxVibrationTime) * 1.2f);
            }
        }
    }

    public virtual void UpdateFunction()
    {
        List<Ripple> ripples = rippleManager.getRipples();
        foreach (Ripple ripple in ripples) {
            if (ripple.isActive
                && Mathf.Abs(calculateDistance(ripple) - ripple.currRadius) < ripple.thickness 
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
