using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour {

    [SerializeField]
    Transform _destination;

    float saveSpeed;

    private GameObject player;
    private GameObject distraction;
    private float timer;
    //private float playerDistance;

    NavMeshAgent _navMeshAgent;
    CharacterFunctionality _characterFunctionality;

    private AudioSource aud;
    //public AudioClip breathing;
    public AudioClip eatDistraction;

	// Use this for initialization
	void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _characterFunctionality = this.GetComponent<CharacterFunctionality>();

        AudioSource aud = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");

        timer = -1.0f;

        saveSpeed = _navMeshAgent.speed;
        SetDestination();
	}

    public void setTimer(float time)
    {
        timer = time;
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
                }
            }
        }

        if (distraction != null)
        {
            _destination = distraction.transform;
        }
        else
        {
            _destination = player.transform;    
        }
        
        if (distraction == null && !player.GetComponent<CharacterFunctionality>().isMoving)
        {
            _characterFunctionality.isMoving = false;
            _navMeshAgent.speed = 0;
        }
        else {
            _characterFunctionality.isMoving = true;
            _navMeshAgent.speed = saveSpeed;
        }

        if(_destination != null) {
            Vector3 targetVector = _destination.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }


    // Update is called once per frame
    void Update () {

        //float playerDistance = player.transform.position - transform.position;
        


        if (timer >= 0) 
        {
            timer -= Time.deltaTime;
            if(timer < 0) {
                Destroy(distraction);
                distraction = null;
            }
        }
	}

    private void FixedUpdate()
    {
        SetDestination();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Distraction") && collision.gameObject.GetComponent<Pickable>().HasBeenThrown())
        {
            distraction = null;
            // sound for eating object
            aud.PlayOneShot(eatDistraction);
            Destroy(collision.gameObject);
        }
    }
}
