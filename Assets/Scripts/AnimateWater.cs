using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWater : MonoBehaviour {

	public Sprite[] frames;
	private float fps = 5;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation ());
	}
	
	// Update is called once per frame
	void Update () {
		fps = 60 * TileSpawner.gameSpeed;
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
