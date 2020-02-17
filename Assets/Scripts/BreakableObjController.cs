using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjController : MonoBehaviour {
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "ThrowingRock(Clone)") {
			coll.gameObject.SetActive (false);
			transform.gameObject.SetActive (false);
		}
	}
}