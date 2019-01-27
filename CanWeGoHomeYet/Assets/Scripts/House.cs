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

	// Use this for initialization
	void Start () {
        importantObjects = GameObject.FindGameObjectsWithTag("Important");
        objectsTriggered = new bool[importantObjects.Length];

        for (int i = 0; i < objectsTriggered.Length; i++) {
            objectsTriggered[i] = false;
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

                        RunSpeechBubble(i);

                        i = objectsTriggered.Length;
                    }
                }

                if (allObjectsFound) {
                    EndGame();
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

    private void RunSpeechBubble(int objectIndex) {
        thoughtBubbleSprite.sprite = importantObjects[objectIndex].GetComponent<ImportantHouseObjects>().associatedSprite;
        thoughtBubbleSprite.gameObject.SetActive(true);

        StartCoroutine(HideSprite());
    }

    //hides the sprite after a few seconds
    IEnumerator HideSprite() {

        yield return new WaitForSeconds(2f);

        thoughtBubbleSprite.gameObject.SetActive(false);
    }

    //resets highlighted object's colour
    IEnumerator ResetHighlight(GameObject highlightedObject) {

        yield return new WaitForEndOfFrame();

        highlightedObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    private void EndGame() {
        SceneManager.LoadScene(nextScene);
    }
}
