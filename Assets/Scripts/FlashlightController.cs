using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour {
	public GameObject spotlight;
	BoxCollider2D spotLightColl;

	void Start () {
		spotLightColl = transform.GetComponent<BoxCollider2D> ();
	}
	void FixedUpdate () {
		if (GameManager.flashlightHasRecharged == true && spotlight.activeSelf) { //True if flashlight is on
			Invoke ("flashlightBatteryDie", 18f);
		}
	}

	void flashlightBatteryDie () { //Simulate battery dying in flashlight for a time (after 10 sec of active light time)
		GameManager.flashlightHasRecharged = false;
		spotlight.SetActive (false);
		spotLightColl.enabled = false;
		Invoke ("flashlightRecharge", 2f);
	}

	void flashlightRecharge () { //Delay before player can turn flashlight back on
		GameManager.flashlightHasRecharged = true;
		spotLightColl.enabled = true;
	}
}