using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    public GameObject[] characters;
    public Vector3 spawnPos;
    private int numEnemies;
    public int enemyDis;

	// Use this for initialization
	void Start () {
        numEnemies = 10;
        //enem
        SetEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetEnemies() {
        int addZ = 10;
        for (int i = 0; i < numEnemies; i++) {
            int ranX = (Random.Range(-1, 2) * 3);
            addZ += 10;
            Instantiate(characters[Random.Range(0, characters.Length)], new Vector3(ranX, 1, addZ), Quaternion.identity);
            //Instantiate(characters[Random.Range(0, characters.Length)], GeneratedPos(), Quaternion.identity);
        }
    }

    Vector3 GeneratedPos() {
        int x = (Random.Range(-1, 2) * 3);
        int y = 1;
        int z =+ 10; //Random.Range(10, 70);
        print(x + "  -  " + y + "  -  " + z);
        return new Vector3(x, y, z);
    }
}
