using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    [Tooltip("Number of goal objects that must be collected to spawn exit portal.")]
    public int numRequiredGoalObjects = 4;

    [Tooltip("Prefab of exit portal to leave level.")]
    public GameObject exitPortal;

    [Tooltip("Location where exit portal will spawn.")]
    public Vector3 exitPortalLocation = new Vector3(0, 1, 0);

    [Tooltip("Sound to play when portal opens.")]
    public AudioClip portalOpenSound;

    int numObtainedWaypoints = 0;
    bool exitPortalSpawned = false;
    AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (numObtainedWaypoints >= numRequiredGoalObjects && !exitPortalSpawned) {
            exitPortalSpawned = true;
            Instantiate(exitPortal, exitPortalLocation, transform.rotation);
            _audioSource.PlayOneShot(portalOpenSound);
        }
	}

    public void incrementObtainedWaypoints() {
        numObtainedWaypoints++;
    }
}
