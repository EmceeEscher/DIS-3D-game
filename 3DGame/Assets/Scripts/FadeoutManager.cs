using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutManager : MonoBehaviour {

    public Image fadeoutImage;
    public float fadeoutTime = 2.0f;
    public AudioClip fadeoutMusic;

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Fadeout()
    {
        audioSource.PlayOneShot(fadeoutMusic);

        Color currentColor = fadeoutImage.color;
        Color visibleColor = fadeoutImage.color;
        visibleColor.a = 1f;

        float counter = 0f;

        while (counter < fadeoutTime)
        {
            counter += Time.deltaTime;
            fadeoutImage.color = Color.Lerp(currentColor, visibleColor, counter / fadeoutTime);
            yield return null;
        }

        //TODO load scene with end credits after this
    }
}
