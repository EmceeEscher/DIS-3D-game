using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctionality : MonoBehaviour {

    // how frequently a ripple will be created / sound effect be played
    public float stepFrequency = 2.0f;
    // maximum radius each ripple will grow to before disappearing
    public float rippleRange = 20.0f;
    // thickness of each individual ripple
    public float rippleThickness = 1.0f;
    // how far in front of the player each ripple will appear
    public float rippleOffset = 0.0f;
    public AudioClip[] footstepClips;


    [HideInInspector]
    public bool isMoving = false;

    RippleManager rippleManager;
    AudioSource audioSource;
    float lastStepTime;
    Vector2 rippleStartPos;

	// Use this for initialization
	void Start () {
        rippleManager = GameObject.FindWithTag("RippleManager").GetComponent<RippleManager>();
        audioSource = GetComponent<AudioSource>();
        lastStepTime = stepFrequency;
        rippleStartPos = new Vector2(transform.position.x, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving && lastStepTime >= stepFrequency) {
            lastStepTime = 0.0f;
            rippleStartPos.x = transform.position.x + transform.forward.x * rippleOffset;
            rippleStartPos.y = transform.position.z + transform.forward.z * rippleOffset;
            rippleManager.CreateRipple(rippleStartPos.x, rippleStartPos.y, rippleRange, rippleThickness, this.tag);
            if(audioSource != null) audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
        }
        lastStepTime += Time.deltaTime;
	}
}
