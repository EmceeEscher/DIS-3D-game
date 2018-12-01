using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Pickable : MonoBehaviour {

    [Tooltip("Radius of ripples created after it's thrown.")]
    public float rippleRadius = 10f;

    [Tooltip("Thickness of ripples created after it's thrown.")]
    public float rippleThickness = 1f;

    [Tooltip("Time between ripples created after it's thrown.")]
    public float timeBetweenRipples = 2f;

    [Tooltip("Maximum time it will last after it's thrown.")]
    public float maxTimeAfterThrown = 20f;

    [Tooltip("Material to use after it's picked up (so it doesn't ripple anymore.")]
    public Material materialAfterPickup;

    [Tooltip("Noise to make when it's picked up.")]
    public AudioClip pickupNoise;

    [Tooltip("Noise to make when it's eaten by the monster")]
    public AudioClip eatenNoise;

    float timeSinceLastRipple = 0f;
    float timeSinceThrown = 0f;

    Collider _collider;
    Rigidbody _rigidbody;
    GameObject soundChild;
    AudioSource _audioSource;
    Renderer _renderer;
    RippleManager _rippleManager;

    bool pickedUp = false;
    bool hasBeenThrown = false;

    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        soundChild = transform.GetChild(0).gameObject;
        _audioSource = soundChild.GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
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
                _rippleManager.CreateRipple(
                                transform.position.x,
                                transform.position.z,
                                rippleRadius,
                                rippleThickness,
                                gameObject.tag);
                timeSinceLastRipple = 0f;
            }
        }
    }

    public bool IsPickedUp() 
    { 
        return pickedUp; 
    }

    public void OnPickup() {

        pickedUp = true;

        // Disable the collider and rigidbody when picked up.
        _rigidbody.isKinematic = true ;

        _renderer.material = materialAfterPickup;

        GetComponentInChildren<LightRippleHandler>().TurnOffPermanently();
    }


    // Used for collision detection with player before having been thrown
    private void OnTriggerEnter(Collider collider)
    {
        // Only gets picked up if player isn't already carrying something
        if (collider.tag == "Player" 
            && !hasBeenThrown 
            && collider.gameObject.GetComponent<ObjectManager>().HasItem() == false)
        {
            _audioSource.PlayOneShot(pickupNoise);
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

        // turn on collider again so it can collide with the ground and monster
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;

        _rigidbody.AddForce(throwForce);
    }

    private void GetEaten()
    {
        // deparent sound child before playing so it will play after parent has been deleted
        soundChild.transform.SetParent(null);
        _audioSource.PlayOneShot(eatenNoise);
        Destroy(soundChild.gameObject, 2f);
        Destroy(gameObject);
    }
}
