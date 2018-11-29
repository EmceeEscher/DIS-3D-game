using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public GameObject item;
    public bool showVision = false;
    public float visionDistance = 5F;
    public LayerMask layerMask;
    private int numItems = 0;
    public static int MAX_ITEMS = 3;
    public GameObject[] inventory = new GameObject[MAX_ITEMS];
            

    // Check whatever is in front. It returns true if it hits an object with the interactable layer. 
    // Interactable objects are eithe r "Switchable" or "Pickable"
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

    private void OnCollisionEnter(Collision collision)
    {
        // if the collided game object is pickable and there are more items to pick up
        item = collision.collider.gameObject;
        if (item.GetComponent<Pickable>() != null && numItems < MAX_ITEMS) {
            // add it to inventory.
            inventory[numItems] = item;
            numItems++;
        }
    }

    // Update is called once per frame
    void Update () {
        // Draws the ray if showVision is true. For debugging. 
        if (showVision == true) {
            Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.red);
        }

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
