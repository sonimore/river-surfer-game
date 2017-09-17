using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	// avoid object animation 
public class avoid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float scaleX = 1 + Mathf.Sin(Time.time * 5) * 0.1f;
		float scaleY = 1 + Mathf.Cos(Time.time * 5) * 0.1f;

		transform.localScale = new Vector3 (scaleX, scaleY, 1);

	}


}
