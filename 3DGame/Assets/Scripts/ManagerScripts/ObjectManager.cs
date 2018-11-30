using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    // Only one object to be held
    public GameObject hand = null;
    public GameObject item;

    public bool showVision = false;
    public float visionDistance = 5F;
    public LayerMask layerMask;
    public float zOffset = 1F, xOffset = 0.5F ;
    public float throwForce = 10F;
            

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
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce + transform.up * throwForce);
        item.GetComponent<Pickable>().Throw();
    }

    public void TurnSwitch(GameObject item) {
        Debug.Log("Switched");
    }

    public bool HasItem() {
        return hand != null;
    }
    void Start () {
            
	}

    private void OnCollisionEnter(Collision collision)
    {
        // if the collided game object is pickable and there are more items to pick up
        if (collision.collider.gameObject.GetComponent<Pickable>() != null 
            && !collision.collider.gameObject.GetComponent<Pickable>().HasBeenThrown()) {
            item = collision.collider.gameObject;
            hand = item;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // if the collided game object is pickable and there are more items to pick up
        if (collider.gameObject.GetComponent<Pickable>() != null
            && !collider.gameObject.GetComponent<Pickable>().HasBeenThrown())
        {
            item = collider.gameObject;
            hand = item;
        }
    }

    private void holdItem(GameObject item) {
        // put item to the position of the player plus a bit to the left.
        item.transform.position = transform.position + (zOffset * transform.forward) + (xOffset * transform.right);
        // locks rotation I think
        item.transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
    }

    // Update is called once per frame
    void Update () {
        // Draws the ray if showVision is true. For debugging. 
        if (HasItem()) {
            holdItem(item);
        }
        if (Input.GetKey(KeyCode.E) && HasItem())
        {
            hand = null;
            Throw(item);

        }


    }
}
