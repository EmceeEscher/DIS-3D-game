using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Pickable : MonoBehaviour {

    public float rippleRadius = 10f;
    public float rippleThickness = 1f;
    public float timeBetweenRipples = 2f;
    public Material materialAfterPickup;

    private float timeSinceLastRipple = 0f;

    private Collider collider;
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private new Renderer renderer;
    private RippleManager rippleManager;

    private bool pickedUp = false;
    private bool hasBeenThrown = false;

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("here0");
        if (hasBeenThrown)
        {
            Debug.Log("here1");
            timeSinceLastRipple += Time.deltaTime;
            if (timeSinceLastRipple > timeBetweenRipples)
            {
                Debug.Log("here2");
                rippleManager.CreateRipple(
                                transform.position.x,
                                transform.position.z,
                                rippleRadius,
                                rippleThickness,
                                gameObject.tag);
                timeSinceLastRipple = 0f;
            }
        }
    }

    public bool IsPickedUp() { return pickedUp; }

    // Communicates to the object that this object is now being hit by the Raycast
    public void OnPickup() {

        pickedUp = true;

        // Disable the collider and rigidbody when picked up.
        rigidbody.isKinematic = true ;

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
            audioSource.Play(0);
            OnPickup();
        }
    }

    public bool HasBeenThrown()
    {
        return hasBeenThrown;
    }

    public void Throw(Vector3 throwForce)
    {
        hasBeenThrown = true;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(throwForce);
        collider.isTrigger = false;
    }

  
}
