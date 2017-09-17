using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {
	private Rigidbody2D tileRB;
	private Collider2D tileCol;
	private float speed = 5f;
	private float currentSpeed = 5f;
	public float bottom = -5f;

	public Sprite[] frames;
	public float fps = 5;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		tileRB = this.GetComponent<Rigidbody2D>();
		tileCol = this.GetComponent<Collider2D>();

		spriteRenderer = this.GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation ());
	}
	
	// Update is called once per frame
	void Update () {
		speed = currentSpeed * TileSpawner.gameSpeed;
		tileRB.velocity = transform.up * -speed;
		if (this.transform.position.y < bottom) {
			Object.Destroy (this.gameObject);
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
