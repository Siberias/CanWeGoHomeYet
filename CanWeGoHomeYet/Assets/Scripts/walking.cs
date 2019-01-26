using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    public GameObject player;
    public GameObject canvas;
    private int lane;
    public bool isWalking;

    private bool inInteraction = false;

	// Use this for initialization
	void Start () {
        //Set lane to 1 (middle)
        lane = 1;

        isWalking = true;

        //Hide Canvas
        canvas.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        //Check walking is true
        if (isWalking == true) {
            //Move Forward
            player.transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime);


            //if press a move left
            if (lane != 0 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))) {
                MoveLeft();
            }
            //if press d move right
            if (lane != 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))) {
                MoveRight();
            }
            //if talking to person
        } else if (inInteraction == true) {
            if (Input.anyKey) {
                //close conversation and go back to walking
                canvas.SetActive(false);
                isWalking = true;
            }
        }
    }

    void MoveRight() {
        player.transform.Translate(new Vector3(3, 0, 0));
        lane++;
    }

    void MoveLeft() {
        player.transform.Translate(new Vector3(-3, 0, 0));
        lane--;
    }

    //walk into enemy
    private void OnTriggerEnter(Collider other) {
        isWalking = false;
        if (other.tag == "Enemy") {
            Destroy(other.gameObject);
            Interaction();
        } else if (other.tag == "Bush") {

        }
    }

    void Interaction() {
        canvas.SetActive(true);
        inInteraction = true;
    }
}
