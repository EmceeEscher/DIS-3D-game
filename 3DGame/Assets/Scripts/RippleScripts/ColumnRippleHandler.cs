using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColumnRippleHandler : ObjectRippleHandler { 
	
    public ColumnRippleHandler()
    {
        Debug.Log("ColumnRippleHandler constructed.");
    }

    public override void Visuals()
    {
        if (isVibrating)
        {
            if (currVibrationTime > maxVibrationTime)
            {
                isVibrating = false;
                renderer.material.SetFloat("_VibrationProgress", -1.0f);
            }
            else
            {
                currVibrationTime += Time.deltaTime;
                renderer.material.SetFloat("_VibrationProgress", (currVibrationTime / maxVibrationTime) * 1.4f);
            }
        }
    }
}
