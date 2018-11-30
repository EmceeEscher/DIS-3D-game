using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour {

    bool hasBeenThrown = false;

    private Collider col;
    private Rigidbody rbdy;

    private bool pickedUp = false;

    public bool IsPickedUp() { return pickedUp; }

    // Communicates to the object that this object is now being hit by the Raycast
    // (see CheckInFront method in ObjectManager)
    public void OnPickup(Collider col, Rigidbody rbdy) {

        pickedUp = true;

        // Disable the collider and rigidbody when picked up.
        rbdy.isKinematic = true ;
    }

    public void Throw()
    {
        hasBeenThrown = true;
    }

    public bool HasBeenThrown()
    {
        return hasBeenThrown;
    }

    // The player would collide with the object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !hasBeenThrown)
        {
            OnPickup(col, rbdy);
        }
    }

    
    // Use this for initialization
    void Start () {
        col = GetComponent<Collider>();
        rbdy = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
