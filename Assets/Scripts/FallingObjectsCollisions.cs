using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsCollisions : MonoBehaviour {
	FallingDownObjectsController objScript;
	ObjectSway swayScript;
	public GameObject objectSwaperoo;
	void Start () {
		objScript = GetComponent<FallingDownObjectsController> ();
		swayScript = GetComponent<ObjectSway> ();
	}
	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.name == "ThrowingRock(Clone)") { // Rock hits CHandelier so we enable the script to make it
			coll.gameObject.SetActive (false);
			gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			objScript.enabled = true;
		}

		if (transform.gameObject.name == "FallingBranch") { //If after our branch falls it collides with ground...set it to inactive
			if (coll.gameObject.name == "Collisions") {
				transform.gameObject.SetActive (false);
			}
		} else {
			if (coll.gameObject.name == "Collisions") {
				objectSwaperoo.SetActive (true);
				swayScript.enabled = false;
				objScript.enabled = false;
				Invoke ("switcherooDelay", 0.5f);

			}
		}
	}

	void switcherooDelay () {
		transform.gameObject.SetActive (false);
	}

	void OnCollisionStay2D (Collision2D coll) { //Player collides with branch so make it break and fall
		if (coll.gameObject.name == "Player") {
			objScript.enabled = true;
		}
	}
}