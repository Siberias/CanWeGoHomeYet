using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour {

    public AudioSource noteAudio;
    public GameObject[] importantObjects;
    private bool[] objectsTriggered;
    public Image thoughtBubbleSprite;
    public string nextScene = "walking";
    public CanvasGroup fader;
    public Image[] thoughtBubble;

	// Use this for initialization
	void Start () {
        importantObjects = GameObject.FindGameObjectsWithTag("Important");
        objectsTriggered = new bool[importantObjects.Length];

        for (int i = 0; i < objectsTriggered.Length; i++) {
            objectsTriggered[i] = false;
        }

        fader.alpha = 0;

        thoughtBubbleSprite.enabled = false;

        for (int i = 0; i < thoughtBubble.Length; i++) {
            thoughtBubble[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity)) {

            if (hit.transform.CompareTag("Important") && Input.GetMouseButtonDown(0)) {

                //if the object hasnt been inspected yet, mark it off
                if (!objectsTriggered[System.Array.IndexOf(importantObjects, hit.transform.gameObject)]) {
                    objectsTriggered[System.Array.IndexOf(importantObjects, hit.transform.gameObject)] = true;
                    noteAudio.Play();
                }

            }

            //if all objects have been inspected, click door and leave
            if (hit.transform.CompareTag("Finish") && Input.GetMouseButtonDown(0)) {

                bool allObjectsFound = true;

                for (int i = 0; i < objectsTriggered.Length; i++) {
                    if (!objectsTriggered[i]) {
                        allObjectsFound = false;

                        StartCoroutine(RunSpeechBubble(i));

                        i = objectsTriggered.Length;
                    }
                }

                if (allObjectsFound) {
                    StartCoroutine(EndGame());
                }
            }


            //if the item is interactable, highlight it
            if ((hit.transform.CompareTag("Important") && !objectsTriggered[System.Array.IndexOf(importantObjects, hit.transform.gameObject)]) || 
                    hit.transform.CompareTag("Finish")) {

                hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                StartCoroutine(ResetHighlight(hit.transform.gameObject));
            }
        }
	}

    //runs the speech bubble
    IEnumerator RunSpeechBubble(int objectIndex) {

        for (int i = 0; i < thoughtBubble.Length; i++) {
            thoughtBubble[i].enabled = true;

            yield return new WaitForSeconds(0.2f);
        }

        thoughtBubbleSprite.sprite = importantObjects[objectIndex].GetComponent<ImportantHouseObjects>().associatedSprite;
        thoughtBubbleSprite.enabled = true;

        yield return new WaitForSeconds(2f);

        thoughtBubbleSprite.enabled = false;

        for (int i = 0; i < thoughtBubble.Length; i++) {
            thoughtBubble[i].enabled = false;
        }
    }

    //resets highlighted object's colour
    IEnumerator ResetHighlight(GameObject highlightedObject) {

        yield return new WaitForEndOfFrame();

        highlightedObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    IEnumerator EndGame() {

        for (float i = 0; i < 1; i += (1f/100f)) {

            fader.alpha = i;

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(nextScene);
    }
}
