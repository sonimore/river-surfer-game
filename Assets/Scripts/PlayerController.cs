using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D playerRB;
	private Collider2D playerCol;
	private float maxSpeed = 5f;
	private float speed = 3f;
	private float currentSpeed;
	public float boundary = 5f;
	public float tapSpeed = 0.1f;
	private float tapTime = 0;
	private float lastTapTime = 0;
	public float restartDelay = 5f;         // Time to wait before restarting the level
	public bool inAir = false;
	private bool facingRight;
	public float maxAirTime = 1;
	public float airTime;
	private float magnifyCounter;
	private Vector3 originalScale;
	private Vector3 originalScale_shadow;
	public float jumpSpeed;
	public float numJumps;

	public GameObject shadow;
	private Rigidbody2D shadowRB;
	public float shadowoffset = -1f;
	private Vector2 offsetDiff;

	public Sprite[] frames;
	public float fps = 5;
	SpriteRenderer spriteRenderer;

	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level
	AudioSource audioSource;

	private Vector3 frometh;
	private Vector3 untoeth;
	private float nextJump = 0;
	private float jumpRate = 1;
	//private float dTime = 0;


	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		playerRB = this.GetComponent<Rigidbody2D>();
		playerCol = this.GetComponent<Collider2D>();
		shadowRB = shadow.GetComponent<Rigidbody2D>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation ());

		magnifyCounter = 0f;
		originalScale = playerRB.transform.localScale;
		originalScale_shadow = shadowRB.transform.localScale;
		offsetDiff = new Vector2(0,shadowoffset);
		currentSpeed = speed;
		shadowRB.transform.localScale = Vector2.zero;
		//farEnd = (0.0f, transform.position.y + 2, 0.0f);

	}

	// Update is called once per frame
	void Update()
	{

		frometh = transform.position;
		if (Input.GetKeyDown(KeyCode.Space) ||
			(playerRB.transform.position.x >= boundary && speed > 0) ||
			(playerRB.transform.position.x <= -boundary && speed < 0))
		{

			speed = -speed;
		}
		//dTime += Time.deltaTime;
        /*
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (((Time.time - lastTapTime) < tapSpeed) && Time.time > nextJump)
			{
				print("Jump");
				Jump();
				nextJump = Time.time + jumpRate;
			}
			lastTapTime = Time.time;

		}
        */
		if (Input.GetKey(KeyCode.Space) && Time.time > nextJump)
		{
			//tapTime += Time.deltaTime;
			tapTime++;
			if (tapTime > 10)
			{
				Jump();
				nextJump = Time.time + jumpRate;
			}

		}
		else
		{
			tapTime = 0;
		}

		/*
        if (playerRB.transform.position.x >= boundary)
		{
			print(playerRB.transform.position.x);
		}
		*/

		if (speed > 0) { facingRight = true; }
		else if (speed < 0) facingRight = false;

		playerRB.velocity = transform.right * speed;
		shadowRB.MovePosition(playerRB.position+offsetDiff);

		if (inAir)
		{
			// airtime
			magnifyCounter = Time.deltaTime;

			// magnify sprite while in air
			float temp_player = 0.5f-0.7f*Mathf.Abs (magnifyCounter - 0.5f);
			playerRB.transform.localScale = new Vector3(temp_player+originalScale.x, temp_player+originalScale.y, 1);
			// shrink shadow while in air
			float temp_shadow = 0.3f+0.2f*Mathf.Abs (magnifyCounter - 0.5f);
			shadowRB.transform.localScale = new Vector3 (temp_shadow + originalScale_shadow.x, temp_shadow + originalScale_shadow.y, 1);

			if ((Time.time - airTime) > maxAirTime)
			{
				inAir = false;
				playerRB.transform.localScale = originalScale;
				playerRB.transform.position = new Vector3(playerRB.position.x,-3,0);
				shadow.transform.localScale = Vector2.zero;
				if (facingRight)
				{
					speed = currentSpeed;
				}
				else if (!facingRight) speed = -maxSpeed;
			}
		}
		speed += .0001f;
	}
	void Jump()
	{

		print("In Air");
		speed = 0;
		inAir = true;
		airTime = Time.time;
		//playerRB.velocity = transform.up * jumpSpeed;
		//transform.position = Vector3.Lerp(frometh, untoeth, Mathf.SmoothStep(0f, Mathf.PingPong(Time.time/secondsForOneLength, 1f) , 0.0f));
		transform.position = new Vector3(frometh.x, -1.5f, 0);

		//


	}


	void OnCollisionEnter2D(Collision2D Obstacle){
		if (Obstacle.gameObject.tag == "Obstacle" && inAir == false) {
			Destroy (gameObject);
			Destroy (shadow);
			//Score.score = 0;
			SceneManager.LoadScene("GameOver");
			//audioSource.Play ();
		}

	}

	IEnumerator PlayAnimation() {
		int currentFrameInd = 0;
		while (currentFrameInd < frames.Length) {
			spriteRenderer.sprite = frames [currentFrameInd];
			yield return new WaitForSeconds (1f / fps);
			currentFrameInd++;
			currentFrameInd = currentFrameInd % (frames.Length);
		}
	}
}
