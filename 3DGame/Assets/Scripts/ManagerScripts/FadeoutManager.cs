using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeoutManager : MonoBehaviour {

    [Tooltip("Image to show on successful ending.")]
    public Image fadeoutEndingImage;

    [Tooltip("Image to show on death.")]
    public Image fadeoutDeathImage;


    [Tooltip("Time for image to fully fade in.")]
    public float fadeoutTime = 2.0f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeoutEnding()
    {
        StartCoroutine(Fadeout(fadeoutEndingImage));
        yield return new WaitForSeconds(fadeoutTime);
        SceneManager.LoadScene(3); // Ending Scene
    }

    public IEnumerator FadeoutDeath()
    {
        StartCoroutine(Fadeout(fadeoutDeathImage));
        yield return new WaitForSeconds(fadeoutTime);
        SceneManager.LoadScene(2); // Death Scene
    }

    public IEnumerator Fadeout(Image fadeoutImage)
    {
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
    }
}
