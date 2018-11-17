using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterRippleHandler : ObjectRippleHandler { 
	
    public MonsterRippleHandler()
    {
        Debug.Log("MonsterRippleHandler constructed.");
    }

    public override void Visuals()
    {
        // change at wiiiiill
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


}
