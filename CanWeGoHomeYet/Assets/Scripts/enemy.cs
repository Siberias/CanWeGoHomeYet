using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public GameObject[] characters;
    public Vector3 spawnPos;
    //private int numEnemies;
    public int enemyDis;

	// Use this for initialization
	void Start () {
        //numEnemies = 10;
        enemyDis = 160 / characters.Length;
        SetEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetEnemies() {
        int addZ = 10;
        for (int i = 0; i < characters.Length; i++) {
            int ranX = (Random.Range(-1, 2) * 3);
            addZ += enemyDis;
            Instantiate(characters[Random.Range(0, characters.Length)], new Vector3(ranX, 1, addZ), Quaternion.identity);
            //Instantiate(characters[Random.Range(0, characters.Length)], GeneratedPos(), Quaternion.identity);
        }
    }

}
