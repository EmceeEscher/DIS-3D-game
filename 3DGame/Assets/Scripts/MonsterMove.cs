using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour {

    [Tooltip("Sound effect to play when eating distraction/player")]
    public AudioClip eatDistraction;

    [SerializeField]
    Transform destination;

    float saveSpeed;

    GameObject player;
    GameObject distraction;
    bool hasEatenPlayer = false;

    NavMeshAgent _navMeshAgent;
    CharacterFunctionality _characterFunctionality;
    Rigidbody _rigidbody;
    FadeoutManager _fadeoutManager;
    AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterFunctionality = GetComponent<CharacterFunctionality>();
        _rigidbody = GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");

        saveSpeed = _navMeshAgent.speed;
        SetDestination();

        _fadeoutManager = GameObject.FindWithTag("FadeoutManager").GetComponent<FadeoutManager>();
	}

    private void SetDestination()
    {
        if (distraction == null)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Distraction"))
            {
                if (item.GetComponent<Pickable>().HasBeenThrown())
                {
                    distraction = item;
                    break;
                }
            }
        }

        if (distraction != null)
        {
            destination = distraction.transform;
        }
        else
        {
            destination = player.transform;    
        }
        
        if (distraction == null && !player.GetComponent<CharacterFunctionality>().isMoving)
        {
            _characterFunctionality.isMoving = false;
            _navMeshAgent.speed = 0;
            _rigidbody.velocity = Vector3.zero;
        }
        else {
            _characterFunctionality.isMoving = true;
            _navMeshAgent.speed = saveSpeed;
        }

        if(destination != null) {
            Vector3 targetVector = destination.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }


    // Update is called once per frame
    void Update () {

    }

    private void FixedUpdate()
    {
        SetDestination();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !hasEatenPlayer)
        {
            _audioSource.PlayOneShot(eatDistraction);
            StartCoroutine(_fadeoutManager.FadeoutDeath());
            hasEatenPlayer = true;
        }
    }
}
