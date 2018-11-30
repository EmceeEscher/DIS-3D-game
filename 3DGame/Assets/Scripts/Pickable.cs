using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour {

    public float rippleRadius = 10f;
    public float rippleThickness = 1f;
    public float timeBetweenRipples = 2f;
    public float timeBeforeDisappearance = 20f;
    public Material materialAfterPickup;

    private float timeSinceLastRipple = 0f;

    private Collider col;
    private Rigidbody rbdy;
    private Renderer renderer;

    private RippleManager rippleManager;

    private bool pickedUp = false;
    private bool hasBeenThrown = false;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider>();
        rbdy = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenThrown)
        {
            timeSinceLastRipple += Time.deltaTime;
            if (timeSinceLastRipple > timeBetweenRipples)
            {
                rippleManager.CreateRipple(
                                transform.position.x,
                                transform.position.z,
                                rippleRadius,
                                rippleThickness,
                                "Throwable");
                timeSinceLastRipple = 0f;
            }
        }
    }

    public bool IsPickedUp() { return pickedUp; }

    // Communicates to the object that this object is now being hit by the Raycast
    public void OnPickup() {

        pickedUp = true;

        // Disable the collider and rigidbody when picked up.
        rbdy.isKinematic = true ;

        renderer.material = materialAfterPickup;
    }

    // The player would collide with the object

    private void OnTriggerEnter(Collider collider)
    {
        // AND If the player is not carrying something
        if (collider.tag == "Player" 
            && !hasBeenThrown 
            && collider.gameObject.GetComponent<ObjectManager>().HasItem() == false)
        {
            OnPickup();
        }
    }

    public void Throw()
    {
        hasBeenThrown = true;
        rbdy.useGravity = true;
        rbdy.isKinematic = false;
        col.isTrigger = false;
    }

    public bool HasBeenThrown()
    {
        return hasBeenThrown;
    }
}
