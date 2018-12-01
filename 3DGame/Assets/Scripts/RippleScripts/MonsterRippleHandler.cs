using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterRippleHandler : ObjectRippleHandler { 

    public override void UpdateFunction()
    {
        List<Ripple> ripples = _rippleManager.getRipples();
        foreach (Ripple ripple in ripples)
        {
            // don't want monster to be affected by its own ripples
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
