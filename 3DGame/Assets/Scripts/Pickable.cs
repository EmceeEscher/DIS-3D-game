using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    private Collider col;
    private Rigidbody rbdy;
    public GameObject player;
    public float zOffset = 1;
    public float xOffset = 1;

    private bool pickedUp = false;

    public bool IsPickedUp() { return pickedUp; }

    // Communicates to the object that this object is now being hit by the Raycast
    // (see CheckInFront method in ObjectManager)
    public void OnPickup(Collider col, Rigidbody rbdy) {

        pickedUp = true;

        // Disable the collider and rigidbody when picked up.
        col.enabled = false;
        rbdy.isKinematic = false;

    }

        
    // The player would collide with the object
    private void OnCollisionEnter(Collision collision)
    {


        // transform this so it is in front of the player
        if (collision.collider.tag == "Player")
        {
            OnPickup(col, rbdy);
            player = collision.collider.gameObject;
        }
    }

    
    // Use this for initialization
    void Start () {
        col = GetComponent<Collider>();
        rbdy = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (IsPickedUp() == true) { OnPickup(col, rbdy); }
        if (player != null) {
            transform.position = player.transform.position + zOffset*player.transform.forward + xOffset*transform.right;
        }
	}
}
