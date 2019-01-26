using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class walking : MonoBehaviour {

    public GameObject player;
    public GameObject canvas;
    private int lane;
    public bool isWalking;
    public Text speaking;

    private bool inInteraction = false;

    private void Awake() {
        GameManager.OnGameStart += GameStart;

    }

    // Use this for initialization
    

	// Use this for initialization
	void GameStart () {
        GameManager.OnGameStart -= GameStart;

        //Set lane to 1 (middle)
        lane = 1;

        isWalking = true;

        //Hide Canvas
        canvas.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        //if press a move left
        if (lane != 0 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))) {
            MoveLeft();
        }
        //if press d move right
        if (lane != 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))) {
            MoveRight();
        }

        //Check walking is true
        if (isWalking == true) {
            //Move Forward
            player.transform.Translate(new Vector3(0, 0, 6) * Time.deltaTime);

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
            ////press button to walk around the fence
            ////if press a move left
            //if (lane != 0 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))) {
            //    MoveLeft();
            //}
            ////if press d move right
            //if (lane != 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))) {
            //    MoveRight();
            //}
            print("bush");
        } else if(other.tag == "School") {
            //end game here
            print("end game");
            isWalking = false;

            StartCoroutine(GameManager.Instance.FadeOut());
        }
    }

    void Interaction() {
        canvas.SetActive(true);
        inInteraction = true;
        string saying = "";

        //randomise what person is saying
        string st = "bcMdJI ZGHBoq rAhijk FDltux ";
        for (int i = 0; i < 10; i++) {
            char c = st[Random.Range(0, st.Length)];
            saying = saying.Insert(0, c.ToString());
        }
        speaking.text = saying;
    }
}
