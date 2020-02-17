using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDoorTriggerShards : MonoBehaviour {
	public GameObject Shard1;
	public GameObject Shard2;
	public GameObject Shard3;
	public GameObject Shard4;
	public GameObject Shard5;
	public GameObject Shard6;
	public GameManager gManager;

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.name == "Player" && gManager.triggerEnd == false) {
			Shard1.SetActive (true);
			Shard2.SetActive (true);
			Shard3.SetActive (true);
			Shard4.SetActive (true);
			Shard5.SetActive (true);
			Shard6.SetActive (true);
		}
	}
}