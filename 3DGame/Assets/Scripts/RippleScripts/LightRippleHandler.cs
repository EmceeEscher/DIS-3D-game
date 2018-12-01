using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRippleHandler : MonoBehaviour {

    public Color baseColor;
    public float transparency = 0.5f;

    Color invisibleColor = new Color(0, 0, 0, 0);
    Color transparentColor;

    Renderer renderer;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        transparentColor = new Color(baseColor.r, baseColor.g, baseColor.b, transparency);
        renderer.material.SetColor("_BaseColor", invisibleColor);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void TurnOnLight()
    {
        renderer.material.SetColor("_BaseColor", transparentColor);
    }

    public void TurnOffLight()
    {
        renderer.material.SetColor("_BaseColor", invisibleColor);
    }

    public void TurnOffPermanently()
    {
        transparentColor = invisibleColor;
    }
}
