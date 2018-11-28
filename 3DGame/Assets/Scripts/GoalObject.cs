using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {

    GoalManager goalManager;
    GameObject player;

	// Use this for initialization
	void Start () {
        goalManager = GameObject.FindWithTag("GoalManager").GetComponent<GoalManager>();
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(player.transform);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            goalManager.incrementObtainedWaypoints();
            Destroy(gameObject);
        }
    }
}
