using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public int numRequiredWaypoints = 4;

    int numObtainedWaypoints = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (numObtainedWaypoints >= numRequiredWaypoints) {
            Debug.Log("you completed the level!");
        }
	}

    public void incrementObtainedWaypoints() {
        numObtainedWaypoints++;
    }
}
