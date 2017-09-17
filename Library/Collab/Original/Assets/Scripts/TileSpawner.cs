using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {

	public float xmin = -5;
	public float xmax = 5;
	public float ymin = -5;
	public float ymax = 5;

	public GameObject purpleTile;
	public GameObject shark;
	public GameObject tree;
	public GameObject coins;

	public float[] treeloc = {6.5f, 7.5f};
	private int treect = 0;
	private int score = 0;
	private float time = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawnTilesCoroutine ());
	}
	
	// Update is called once per frame
	void Update () {
		treect = (treect + 1) % 2;
		score = Score.score;
		time = Time.time;
		print (time);
	}

	IEnumerator spawnTilesCoroutine() {
		GameObject dummy;
		while (true) {
			yield return new WaitForSeconds (0.8f);
			Vector3 pos0 = new Vector3 (Random.Range (xmin, xmax), Random.Range (ymin, ymax), 0);
			Vector3 pos1 = new Vector3 (Random.Range (xmin, xmax), Random.Range (ymin, ymax), 0);
			Vector3 treePos0 = new Vector3 (treeloc[treect], 6, 0);
			Vector3 treePos1 = new Vector3 (-treeloc[treect], 6, 0);
			int ind = Random.Range (0, 30) % 6;

			if (time < 45) {
				switch (ind) {
				case 0:
				case 1:
					Instantiate (coins, pos0, Quaternion.identity);
					dummy = (Random.Range(0,10000000) == 0) ? Instantiate (coins, pos0, Quaternion.identity) : Instantiate (coins, pos0, Quaternion.identity);
					break;
				case 2:
					if (time > 5) {
						Instantiate (purpleTile, pos0, Quaternion.identity);
					}
					break;
				case 3:
				case 4:
					if (time > 15) {
						Instantiate (shark, pos0, Quaternion.identity);
					}
					dummy = (Random.Range (0, 2) % 2 == 0) ? Instantiate (tree, treePos0, Quaternion.identity) : Instantiate (tree, treePos1, Quaternion.identity);
					break;
				case 5:
					if (time > 25) {
						Instantiate (purpleTile, pos0, Quaternion.identity);
					}
					dummy = (Random.Range (0, 1) % 2 == 0) ? Instantiate (tree, treePos0, Quaternion.identity) : Instantiate (tree, treePos1, Quaternion.identity);
					break;
				default:
					//Instantiate (purpleTile, pos0, Quaternion.identity);
					dummy = (Random.Range (0, 1) % 2 == 0) ? Instantiate (tree, treePos0, Quaternion.identity) : Instantiate (tree, treePos1, Quaternion.identity);
					break;
				}
			} else {
				if (Random.Range (0, 3) % 3 != 0) {
					Instantiate (purpleTile, pos0, Quaternion.identity);
				}
				if (Random.Range (0, 3) % 3 != 1) {
					Instantiate (shark, pos1, Quaternion.identity);
				}

				/*
				if (Random.Range(0,100000) ==0){
					Instantiate (coins, pos0, Quaternion.identity);
					
				}
				*/
			}
		}
	}
}
