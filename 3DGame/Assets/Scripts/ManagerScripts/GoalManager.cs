using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public int numRequiredWaypoints = 4;
    public GameObject exitPortal;
    public AudioClip portalOpenSound;
    public AudioClip objectColletionSound;

    int numObtainedWaypoints = 0;
    bool exitPortalSpawned = false;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (numObtainedWaypoints >= numRequiredWaypoints && !exitPortalSpawned) {
            exitPortalSpawned = true;
            Instantiate(exitPortal, new Vector3(0, 1, 0), transform.rotation);
            audioSource.PlayOneShot(portalOpenSound);
        }
	}

    public void incrementObtainedWaypoints() {
        numObtainedWaypoints++;
        if (numObtainedWaypoints < numRequiredWaypoints) {
            audioSource.PlayOneShot(objectColletionSound);
        }
    }
}
