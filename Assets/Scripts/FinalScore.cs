using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {

    GameObject txt;
    Score scoreScript;
    private int endScore;
    public Text scoreTxt;

    // Use this for initialization
    void Start () {

        transform.localPosition = new Vector3(0, -250, 0);
        txt = GameObject.Find("Text");
        scoreScript = txt.GetComponent<Score>();
        endScore = Score.score;
        scoreTxt.text = "Final Score: " + endScore.ToString();
        Score.score = 0; 
		TileSpawner.gameSpeed = 1;
		TileSpawner.time = 0;
        

    }
	
	// Update is called once per frame
	void Update () {

    }
}
