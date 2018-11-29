using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanel : MonoBehaviour {

    public GameObject nextPage;
    public GameObject prevPage;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadPrevPage()
    {
        gameObject.SetActive(false);
        prevPage.SetActive(true);
    }

    public void LoadNextPage()
    {
        gameObject.SetActive(false);
        nextPage.SetActive(true);
    }
}
