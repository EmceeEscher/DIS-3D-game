using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRippleHandler : MonoBehaviour {

    public Color baseColor;
    public float transparency = 0.5f;

    Color invisibleColor = new Color(0, 0, 0, 0);
    Color transparentColor;

    Renderer _renderer;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        transparentColor = new Color(baseColor.r, baseColor.g, baseColor.b, transparency);
        _renderer.material.SetColor("_Color", invisibleColor);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void TurnOnLight()
    {
        _renderer.material.SetColor("_Color", transparentColor);
    }

    public void TurnOffLight()
    {
        _renderer.material.SetColor("_Color", invisibleColor);
    }

    public void TurnOffPermanently()
    {
        transparentColor = invisibleColor;
    }
}
