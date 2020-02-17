using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisions : MonoBehaviour {
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "ThrowingRock(Clone)") {
			coll.gameObject.SetActive (false);
			GetComponent<Rigidbody2D> ().gravityScale = 5.0f; //Enable gravity if hit by rock, forcing key to fall from branch
		}

	}

	void FixedUpdate () {
		Physics2D.IgnoreLayerCollision (5, 11, true);
		Physics2D.IgnoreLayerCollision (9, 11, true);
		if (GameManager.finalEnemyCount == 0) {
			GetComponent<Rigidbody2D> ().gravityScale = 5.0f;
		}
	}
}