using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Pickable : MonoBehaviour {

    public float rippleRadius = 10f;
    public float rippleThickness = 1f;
    public float timeBetweenRipples = 2f;
    public float maxTimeAfterThrown = 20f;
    public Material materialAfterPickup;
    public AudioClip pickupNoise;
    public AudioClip eatenNoise;

    private float timeSinceLastRipple = 0f;
    private float timeSinceThrown = 0f;

    private Collider collider;
    private Rigidbody rigidbody;
    private GameObject soundChild;
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
        soundChild = transform.GetChild(0).gameObject;
        audioSource = soundChild.GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenThrown)
        {
            timeSinceLastRipple += Time.deltaTime;
            timeSinceThrown += Time.deltaTime;
            if (timeSinceThrown > maxTimeAfterThrown)
            {
                GetEaten();
            }
            else if (timeSinceLastRipple > timeBetweenRipples)
            {
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


    // Used for collision detection with player before having been thrown
    private void OnTriggerEnter(Collider collider)
    {
        // Only gets picked up if player isn't already carrying something
        if (collider.tag == "Player" 
            && !hasBeenThrown 
            && collider.gameObject.GetComponent<ObjectManager>().HasItem() == false)
        {
            audioSource.PlayOneShot(pickupNoise);
            OnPickup();
        }
    }

    // Used for collision detection with monster after having been thrown
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Monster" && hasBeenThrown)
        {
            GetEaten();
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

    private void GetEaten()
    {
        soundChild.transform.SetParent(null);
        audioSource.PlayOneShot(eatenNoise);
        Destroy(soundChild.gameObject, 2f);
        Destroy(gameObject);
    }
}
