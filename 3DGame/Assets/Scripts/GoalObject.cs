using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {

    GoalManager goalManager;

	// Use this for initialization
	void Start () {
        goalManager = GameObject.FindWithTag("GoalManager").GetComponent<GoalManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            goalManager.incrementObtainedWaypoints();
            Destroy(gameObject);
        }
    }
}
