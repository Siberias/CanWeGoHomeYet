using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    public GameObject player;
    private int lane;
    public bool walking;

	// Use this for initialization
	void Start () {
        //Set lane to 1 (middle)
        lane = 1;

        walking = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (walking == true) {
            //Move Forward
            player.transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime);
        }

        //if press a move left
        if (lane != 0 && (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))) {
            MoveLeft();
        }
        //if press d move right
        if (lane != 2 && (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))) {
            MoveRight();
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
        walking = false;
    }
}
