using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour {

    GoalManager goalManager;
    GameObject player;
    GameObject soundChild; // child
    AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        goalManager = GameObject.FindWithTag("GoalManager").GetComponent<GoalManager>();
        player = GameObject.FindWithTag("Player");
        soundChild = transform.GetChild(0).gameObject;
        _audioSource = soundChild.GetComponent<AudioSource>(); 
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            // deparent audio so it can play even when the parent object is destroyed
            soundChild.transform.SetParent(null);
            _audioSource.Play();
            Destroy(soundChild.gameObject, 2f);

            goalManager.incrementObtainedWaypoints();
            Destroy(gameObject);
        }
    }
}
