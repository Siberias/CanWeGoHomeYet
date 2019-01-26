using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public string nextScene;

    //clock variables
    [SerializeField]
    private Text clock;
    private int totalMinutes;
    private float minuteTimer;
    private float minuteTime = 1;

    //fading in variables
    [SerializeField]
    private CanvasGroup fader;
    private int framesToLoad = 30;

    public static GameManager Instance;


	// Use this for initialization
	void Start () {

		if (!Instance) {
            Instance = this;
        } else {
            Destroy(this);
        }

        StartCoroutine(FadeIn());

        //set up clock variables
        totalMinutes = 9 * 60;
        minuteTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (minuteTimer <= Time.time) {

            totalMinutes++;
            minuteTimer = Time.time + minuteTime;

            clock.text = ConvertMinutes();
        }
	}

    //change to the next scene
    public void ChangeScene() {
        SceneManager.LoadScene(nextScene);
    }

    //convert minutes to digital time as string
    private string ConvertMinutes() {

        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;
        string returnTime;
               
        if (hours < 10) {
            returnTime = "0" + hours + ":";
        } else {
            returnTime = hours + ":";
        }

        if (minutes < 10) {
            returnTime += "0" + minutes;
        } else {
            returnTime += minutes;
        }

        return returnTime;
    }

    //fade into the game
    IEnumerator FadeIn() {

        for (float i = 1; i > 0; i -= (1f/30f)) {
            fader.alpha = i;

            yield return new WaitForEndOfFrame();
        }
    }

    //fade into the game
    IEnumerator FadeOut() {

        for (float i = 0; i < 1; i += (1f / 30f)) {
            fader.alpha = i;


            yield return new WaitForEndOfFrame();
        }


    }
}
