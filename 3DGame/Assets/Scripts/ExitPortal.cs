using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour {

    GameObject player;
    FadeoutManager fadeoutManager;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        fadeoutManager = GameObject.FindWithTag("FadeoutManager").GetComponent<FadeoutManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            StartCoroutine(fadeoutManager.Fadeout());
        }    
    }
}
