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

    NavMeshAgent _navMeshAgent;
    CharacterFunctionality _characterFunctionality;

	// Use this for initialization
	void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _characterFunctionality = this.GetComponent<CharacterFunctionality>();

        player = GameObject.FindWithTag("Player");

        saveSpeed = _navMeshAgent.speed;
        SetDestination();
	}

    private void SetDestination()
    {
        if (!player.GetComponent<CharacterFunctionality>().isMoving)
        {
            _characterFunctionality.isMoving = false;
            _navMeshAgent.speed = 0;
        }
        else {
            _characterFunctionality.isMoving = true;
            _navMeshAgent.speed = saveSpeed;
        }

        if(_destination != null) {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }


    // Update is called once per frame
    void Update () {
        SetDestination();
	}
}
