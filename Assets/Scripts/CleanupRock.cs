using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupRock : MonoBehaviour {
	void Update () {
		Invoke ("Cleanup", 2.75f);
	}

	void Cleanup () {
		transform.gameObject.SetActive (false);
	}
	void OnCollision2D (Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			transform.gameObject.SetActive (false);
		}
	}
}