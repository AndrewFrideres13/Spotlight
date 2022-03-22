using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlashlightGyroRotation : MonoBehaviour {
	public PlayerController playerObj;
	public AudioClip batteryPickupSound;
	public GameManager gManager;
	private bool flashlightSizeHasChanged = false;
	private GameObject flashlightFX;
	[Range (20, 150)] private Quaternion localRotation;
	void Start () {
		localRotation = transform.rotation;
		flashlightFX = gameObject.transform.GetChild (0).gameObject;
	}
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "battery_pickup") {
			coll.gameObject.SetActive (false); //Disables rather than destroys for memory sake...
			gManager.flashlightSize += new Vector3 (5, 5, 0); //Scales our flashlight up
			flashlightSizeHasChanged = true;
			AudioSource.PlayClipAtPoint (batteryPickupSound, transform.position);
		}
	}
	void FixedUpdate () {
		if (flashlightSizeHasChanged == true) {
			flashlightSizeHasChanged = false;
			flashlightFX.transform.localScale = gManager.flashlightSize; //Need to get child of this since we're on the arm in this script now
		}
		Physics2D.IgnoreLayerCollision (0, 9, true);
		if (playerObj.GetDirection () == true) { //Facing right
			if ((transform.eulerAngles.z > 55 && transform.eulerAngles.z < 120)) {
				localRotation.z += Mathf.Clamp ((-Input.acceleration.x * Time.deltaTime) * 3.5f, -10f, 120f); //Clamp it to 10 below the z = 90 position as min, and about 120 as the max
			}
		}
		if (playerObj.GetDirection () == false) { //Facing left
			if ((transform.eulerAngles.z > 55 && transform.eulerAngles.z < 120)) {
				localRotation.z += Mathf.Clamp ((-Input.acceleration.x * Time.deltaTime) * 3.5f, -120f, 10f); //Clamp it to 10 below the z = 90 position as min, and about 120 as the max
			}
		}
		if (transform.eulerAngles.z > 90) {
			localRotation.z -= 0.4f * Time.deltaTime;
		} else if (transform.eulerAngles.z < 90) {
			localRotation.z += 0.4f * Time.deltaTime;
		} else if (transform.eulerAngles.z > 88.5 && transform.eulerAngles.z < 91.5) {
			localRotation.z += 0; // Kill the addition of movement so that we dont get any weird stutter
		}
		transform.rotation = localRotation;
		//220, 300 UPPER LOWER FOR LEFT SIDE
	}
}