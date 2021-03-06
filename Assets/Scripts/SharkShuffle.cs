using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShuffle : MonoBehaviour
{
	private Rigidbody2D tileRB;
	private Collider2D tileCol;
	public float boundary = 5f;
	private float speed = 5f;
	private float currentSpeed = 5f;
	private float bottom = -5f;
	private float hspeed = 2f;
	private bool moveRight = true;
	private Vector2 hvec;
	private Vector2 vvec;

	public Sprite[] frames;
	public float fps = 5;
	SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start()
	{
		tileRB = this.GetComponent<Rigidbody2D>();
		tileCol = this.GetComponent<Collider2D>();
		hvec = transform.right * hspeed;
		vvec = transform.up * -speed;
		tileRB.velocity = vvec + hvec;

		// randomly start as moving left or right
		int dir = Random.Range (0, 50) % 2;
		moveRight = dir == 0 ? true : false;

		spriteRenderer = this.GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation ());
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		speed = currentSpeed * TileSpawner.gameSpeed;
		if ((tileRB.transform.position.x >= boundary) ||
		    (tileRB.transform.position.x <= -boundary))
			switchDirections ();

		if (moveRight) {
			hvec = transform.right * hspeed;
		} else {
			hvec = transform.right * -hspeed;
		}
		vvec = transform.up * -speed;
		tileRB.velocity = vvec + hvec;

		if (this.transform.position.y < bottom) {
			Object.Destroy (this.gameObject);
		}
	}
	void OnCollisionEnter2D(Collision2D Obstacle){
		if (Obstacle.gameObject.tag == "Obstacle") {
			switchDirections ();
		}
	}
	void switchDirections(){
		moveRight = !moveRight;
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
