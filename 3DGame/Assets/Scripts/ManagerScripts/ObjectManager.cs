using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
    // Show if can be picked up. Highlight?
    // Listen for input. 
    // Use this for initialization

    private GameObject item;
    public bool showVision = false;
    public float visionDistance = 5F;
    public LayerMask layerMask;

    // Check whatever is in front. It returns true if it hits an object with the interactable layer. 
    // Interactable objects are either "Switchable" or "Pickable"
    GameObject CheckInFront() {
        RaycastHit hit;
        // Raycast checking if there is an object (that has 'interactable' layer) from visiondistance, 
        // highlight this object.
        if (Physics.Raycast(transform.position, transform.forward, out hit, visionDistance, layerMask)) {
            Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.blue);


            return hit.collider.gameObject;
        }
        return null;
    }

    public void Throw(GameObject item) {
        
    }

    public void TurnSwitch(GameObject item) {
        Debug.Log("Switched");
    }

    void Start () {
            
	}
	
	// Update is called once per frame
	void Update () {
        // Draws the ray if showVision is true. For debugging. 
        if (showVision == true) {
            Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.red);
        }

        // Sets item to be whatever is interacting with the ray. If false, it's null.

        // Press E to interact. 


        // If the item is picked up and it is a type of Pickable, then pressing E will throw that item
        if (Input.GetKey(KeyCode.E))
        {
            item = CheckInFront();
            if (item != null)
            {
                if (item.GetComponent<Pickable>() != null) Throw(item);
                else if (item.GetComponent<Switchable>() != null) TurnSwitch(item);
            }
        }



    }
}
