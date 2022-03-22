using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FShardController : MonoBehaviour {
	private Vector3 fShards;
	private float shardDownwardPos;
	public GameManager gManager;
	void Start () {
		fShards = transform.position;
		shardDownwardPos = transform.position.y - 7.0f; //Get a pos downwar of 7 from each shard (so they all dissapear at once)
	}

	void FixedUpdate () { //SLowly lower shards and deactivate them when they reach their slots in the Oculus
		fShards.y -= 1.3f * Time.deltaTime;
		transform.position = fShards;
		if (transform.position.y <= shardDownwardPos) {
			gManager.triggerEnd = true;
			transform.gameObject.SetActive (false);
		}
	}
}