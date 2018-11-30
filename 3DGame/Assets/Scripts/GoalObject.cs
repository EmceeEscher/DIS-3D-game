using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {

    GoalManager goalManager;
    GameObject player;
    GameObject sound; // child
    private AudioSource aud;

	// Use this for initialization
	void Start () {
        goalManager = GameObject.FindWithTag("GoalManager").GetComponent<GoalManager>();
        player = GameObject.FindWithTag("Player");
        sound = transform.GetChild(0).gameObject;
        aud = sound.GetComponent<AudioSource>(); 
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(player.transform);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            // deparent audio so it can play even when the parent object is destroyed
            sound.transform.SetParent(null);
            aud.Play();
            Destroy(sound.gameObject, 2f);

            goalManager.incrementObtainedWaypoints();
            Destroy(gameObject);
        }
    }
}
