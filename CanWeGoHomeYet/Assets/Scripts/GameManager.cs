using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string nextScene;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //change to the next scene
    public void ChangeScene() {
        SceneManager.LoadScene(nextScene);
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
