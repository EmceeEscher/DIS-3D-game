using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectRippleHandler : MonoBehaviour {

    public RippleManager rippleManager;
    public float maxVibrationTime = 5.0f;

    Renderer renderer;
    float currVibrationTime = 0.0f;
    bool isVibrating = false;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_VibrationProgress", -1.0f);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        renderer.material.SetFloat("_MaxMeshY", mesh.bounds.max.y);
    }

    // Update is called once per frame
    void Update()
    {
        List<Ripple> ripples = rippleManager.getRipples();
        foreach (Ripple ripple in ripples) {
            if (Mathf.Abs(calculateDistance(ripple) - ripple.currRadius) < ripple.thickness && !isVibrating) {
                isVibrating = true;
                currVibrationTime = 0.0f;
            }
        }

        if (isVibrating) {
            if (currVibrationTime > maxVibrationTime)
            {
                isVibrating = false;
                renderer.material.SetFloat("_VibrationProgress", -1.0f);
            }
            else
            {
                currVibrationTime += Time.deltaTime;
                renderer.material.SetFloat("_VibrationProgress", (currVibrationTime / maxVibrationTime) * 1.2f);
            }
        }
    }

    float calculateDistance(Ripple ripple) {
        return Mathf.Sqrt(
            Mathf.Pow((ripple.centerX - transform.position.x), 2) + 
            Mathf.Pow((ripple.centerZ - transform.position.z), 2));
    }
}
