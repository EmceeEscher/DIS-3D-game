using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ObjectManager : MonoBehaviour {

    // Object currently being held
    [HideInInspector]
    public GameObject hand;

    [Tooltip("How far away from the player items are held.")]
    public Vector3 heldOffset = new Vector3(0.5f, 0.5f, 1.0f);

    [Tooltip("How hard items are thrown.")]
    public float throwForce = 10F;
            
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (HasItem())
        {
            HoldItem(hand);
        }
        // if E is pressed and there's something in hand, throw it
        if (Input.GetKey(KeyCode.E) && HasItem())
        {
            GameObject item = hand;
            Throw(item);
            hand = null;
        }
    }

    public bool HasItem()
    {
        return hand != null;
    }

    void Throw(GameObject item) { 
        item.GetComponent<Pickable>().Throw(transform.forward * throwForce + transform.up * throwForce);
    }

    void OnTriggerEnter(Collider collider)
    {
        // should only pickup if hand is empty, object is pickable, and it hasn't already been thrown.
        if (collider.gameObject.GetComponent<Pickable>() != null
            && !collider.gameObject.GetComponent<Pickable>().HasBeenThrown()
            && HasItem() == false)
        {
            hand = collider.gameObject;
        }
    }

    void HoldItem(GameObject item) {
        // put item to the position of the player plus a bit out, up, and to the right so it's visible.
        item.transform.position = transform.position
            + (heldOffset.z * transform.forward)
            + (heldOffset.x * transform.right)
            + (heldOffset.y * transform.up);

        // locks rotation of pickable
        item.transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
    }

}
