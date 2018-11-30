using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterRippleHandler : ObjectRippleHandler { 

    public override void Visuals()
    {
        // change at wiiiiill
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
                renderer.material.SetFloat("_VibrationProgress", (currVibrationTime / maxVibrationTime) * 1.2f);
            }
        }
    }

    public override void UpdateFunction()
    {
        List<Ripple> ripples = rippleManager.getRipples();
        foreach (Ripple ripple in ripples)
        {
            if (ripple.isActive
                && Mathf.Abs(calculateDistance(ripple) - ripple.currRadius) < (ripple.thickness + modelRadius)
                && !isVibrating
                && ripple.sourceTag != "Monster")
            {
                isVibrating = true;
                currVibrationTime = 0.0f;
            }
        }

        Visuals();
    }
}
