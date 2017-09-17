using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleTileScroll : MonoBehaviour
{
	public Rigidbody2D tileRB;
	private Collider2D tileCol;
	private float speed = 5f;
	private float currentSpeed = 5f;
	public float bottom = -5f;

	// Use this for initialization
	void Start()
	{
		tileRB = this.GetComponent<Rigidbody2D>();
		tileCol = this.GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		speed = currentSpeed*TileSpawner.gameSpeed;
		tileRB.velocity = transform.up * -speed;
		if (this.transform.position.y < bottom) {
			Object.Destroy (this.gameObject);
		}
	}
}
