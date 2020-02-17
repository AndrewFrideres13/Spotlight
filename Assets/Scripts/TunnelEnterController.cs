using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEnterController : MonoBehaviour {
	public GameObject player;

	public GameObject tunnelExit;
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero; //Freeze after tele to avoid falling off/Confusion
			player.transform.position = tunnelExit.transform.position;
		}
	}
}