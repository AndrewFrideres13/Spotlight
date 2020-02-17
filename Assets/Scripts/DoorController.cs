using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public GameManager gManager;
	public GameObject openDoorObj;
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			if (gManager.HaveKey == true) {
				openDoorObj.SetActive (true);
				transform.gameObject.SetActive (false);
				gManager.HaveKey = false;
			}
		}
	}
}