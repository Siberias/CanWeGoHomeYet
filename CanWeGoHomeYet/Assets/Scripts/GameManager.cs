using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public string nextScene;

    //clock variables
    [SerializeField]
    private GameObject clockPanel1, clockPanel2;
    [SerializeField]
    private Text clock;
    private int minFontSize = 36;
    private int maxFontSize = 200;
    private int totalMinutes;
    private float minuteTimer;
    private float minuteTime = 1;

    //fading in variables
    [SerializeField]
    private CanvasGroup fader;
    private int framesToLoad = 100;
    [SerializeField]
    private Text countdownText;
    [SerializeField]
    private GameObject countDownPanel;

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

        //calculate time in hours and minutes
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

        //set the clock to be large and in the centre of the screen
        clock.fontSize = (int)maxFontSize;
        Vector3 initialPos = clock.transform.parent.transform.parent.TransformPoint(new Vector3(0, Screen.height / 4, 0));
        clock.transform.position = initialPos;

        for (float i = 1; i > 0; i -= (1f/framesToLoad)) {
            fader.alpha = i;

            clock.fontSize = (int)Mathf.Lerp(maxFontSize, minFontSize, 1 - i);
            clock.transform.position = Vector3.Lerp(initialPos, clockPanel1.transform.position, 1 - i);

            yield return new WaitForEndOfFrame();
        }

        //make clock background visible
        clockPanel1.SetActive(true);
        clockPanel2.SetActive(true);

        //countdown
        for (int i = 3; i >= 0; i--) {

            if ( i != 0) {
                countdownText.text = i.ToString();
            } else {
                countdownText.text = "GO!";
            }

            yield return new WaitForSeconds(0.75f);

        }

        countDownPanel.SetActive(false);
        countdownText.text = "";
    }

    //fade into the game
    public IEnumerator FadeOut() {

        //set the clock to be large and in the centre of the screen
        Vector3 endPos = clock.transform.parent.transform.parent.TransformPoint(new Vector3(0, Screen.height / 4, 0));

        //make clock background invisible
        clockPanel1.SetActive(false);
        clockPanel2.SetActive(false);

        for (float i = 0; i < 1; i += (1f / framesToLoad)) {
            fader.alpha = i;

            clock.fontSize = (int)Mathf.Lerp(minFontSize, maxFontSize, i);
            clock.transform.position = Vector3.Lerp(clockPanel1.transform.position, endPos, i);

            yield return new WaitForEndOfFrame();
        }
    }
}
