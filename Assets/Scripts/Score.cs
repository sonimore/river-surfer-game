using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    GameObject player;
    PlayerController playerctrl;
    GameObject spawner;
    TileSpawner tileSpawner;
    public static int score = 0;
    public float scoreStorage;
    public int baseScore;
    public Text scoreText;
    private bool grounded;
    private float timer;
    private float maxTimer = 0.5f;
    private float gspeed;

    // Use this for initialization
    void Start () {


        transform.localPosition = new Vector3(280, 240, 0);
        player = GameObject.Find("Player");
        playerctrl = player.GetComponent<PlayerController>();
        spawner = GameObject.Find("Spawner");
        tileSpawner = spawner.GetComponent<TileSpawner>();

    }

	
	// Update is called once per frame
	void Update () {

        gspeed = TileSpawner.gameSpeed;
        grounded = !playerctrl.inAir;
        scoreText.text = "Score: " + score.ToString();
        scoreStorage = 10*Mathf.Pow(gspeed, 1.5f);
        baseScore = (int)scoreStorage;
        if (grounded)
        {
            if ((Time.time - timer) > maxTimer)
            {
                timer = Time.time;
                score += baseScore;
            }
            

        }

    }
}
