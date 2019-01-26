using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    public GameObject player;
    private int lane;

	// Use this for initialization
	void Start () {
        //Set lane to 1 (middle)
        lane = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //Move Forward
        player.transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime);

        //if press a move left
        if (Input.GetKeyDown("a") && lane != 0) {
            MoveLeft();
        }
        //if press d move right
        if (Input.GetKeyDown("d") && lane != 2) {
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
}
