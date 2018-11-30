using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRippleHandler : MonoBehaviour {

    public Color playerRippleColor;
    public Color monsterRippleColor;
    public Color throwableRippleColor;

    RippleManager rippleManager;
    int maxNumRipples;
    Renderer renderer;
    Vector4[] rippleData;
    Color[] rippleColors;
    Color transparent = new Color(0f, 0f, 0f, 0f);

	// Use this for initialization
	void Start () {
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();

        maxNumRipples = rippleManager.maxNumRipples;

        renderer = GetComponent<Renderer>();

        rippleData = new Vector4[maxNumRipples];
        rippleColors = new Color[maxNumRipples];
        for (int i = 0; i < maxNumRipples; i++) {
            rippleData[i] = new Vector4(0f, 0f, 0f, 0f);
            rippleColors[i] = transparent;
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
                if (ripple.sourceTag == "Player")
                {
                    rippleColors[i] = playerRippleColor;
                } 
                else if (ripple.sourceTag == "Monster")
                {
                    rippleColors[i] = monsterRippleColor;
                }
                else
                {
                    rippleColors[i] = throwableRippleColor;
                }
            } else {
                rippleData[i].x = 0f;
                rippleData[i].z = 0f;
                rippleData[i].y = 0f;
                rippleData[i].w = 0f;
                rippleColors[i] = transparent;
            }
        }
        renderer.material.SetVectorArray("_Ripples", rippleData);
        renderer.material.SetColorArray("_RippleColors", rippleColors);
	}
}
