using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public int numRequiredWaypoints = 4;
    public GameObject exitPortal;

    int numObtainedWaypoints = 0;
    bool exitPortalSpawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (numObtainedWaypoints >= numRequiredWaypoints && !exitPortalSpawned) {
            exitPortalSpawned = true;
            Instantiate(exitPortal, transform.position, transform.rotation);
        }
	}

    public void incrementObtainedWaypoints() {
        numObtainedWaypoints++;
    }
}
