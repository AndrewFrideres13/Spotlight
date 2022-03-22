using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallingDownObjectsController : MonoBehaviour {
	float objectPos = 0;
	void Start () {
		objectPos = transform.localPosition.y;
	}

	void FixedUpdate () { //Brings object down
		GetComponent<Rigidbody2D> ().gravityScale = 1.5f;
		objectPos -= 25.0f * Time.deltaTime;
		transform.localPosition = new Vector3 (transform.localPosition.x, objectPos, transform.localPosition.z);
	}
}