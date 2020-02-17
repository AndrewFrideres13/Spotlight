using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
	public GameObject player;
	void FixedUpdate () {
		transform.position = player.transform.position; //Glues the severed arm/Flashlight to players arm...slot?
	}
}