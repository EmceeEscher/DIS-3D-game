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

    GameObject CheckInFront() {
        RaycastHit hit;
        // Raycast checking if there is an object (that has 'interactable' layer) from visiondistance, 
        // highlight this object.
        if (Physics.Raycast(transform.position, transform.forward, out hit, visionDistance, layerMask)) {
            Debug.Log("Did hit");
            Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.yellow);

            // Assumes that layer masks only selects objects that are throwable.
            return hit.collider.gameObject;
        }
        return null;
    }
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // Draws the ray if showVision is true. For debugging
        if (showVision == true) {
            Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.red);
        }

        // Sets item to be whatever is interacting with the ray. If false, it's null.
        item = CheckInFront();

        if (Input.GetKey("E") && item.GetComponent("Pickable") {
            item.Throw();
        }

    }
}
