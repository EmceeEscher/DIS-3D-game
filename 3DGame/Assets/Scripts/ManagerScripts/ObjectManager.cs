using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ObjectManager : MonoBehaviour {

    // Only one object to be held
    public GameObject hand = null;

    public float visionDistance = 5F;
    public LayerMask layerMask;
    public float zOffset = 1F, xOffset = 0.5F, yOffset = 0.5F;
    public float throwForce = 10F;
            

    void Start() {

    }

    public void Throw(GameObject item) { 
        item.GetComponent<Pickable>().Throw(transform.forward * throwForce + transform.up * throwForce);
    }


    public bool HasItem() {
        return hand != null;
    }


    private void OnTriggerEnter(Collider collider)
    {
        // if hand is empty, object is pickable, and it hasn't been thrown.
        if (collider.gameObject.GetComponent<Pickable>() != null
            && !collider.gameObject.GetComponent<Pickable>().HasBeenThrown()
            && HasItem() == false)
        {

            hand = collider.gameObject;
        }
    }

    private void HoldItem(GameObject item) {
        // put item to the position of the player plus a bit to the left.
        item.transform.position = transform.position 
            + (zOffset * transform.forward) 
            + (xOffset * transform.right) 
            + (yOffset * transform.up);
        // locks rotation I think
        item.transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
    }

    // Update is called once per frame
    void Update () {

        if (HasItem()) {
            HoldItem(hand);
        }
        // if E is pressed and there's something in hand,
        if (Input.GetKey(KeyCode.E) && HasItem())
        {
            GameObject item = hand;
            Throw(item);
            hand = null;
        }
    }
}
