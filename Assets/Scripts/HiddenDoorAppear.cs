using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoorAppear : MonoBehaviour {
	private Collider2D hDoor;
	private bool ignorePlayer = true;
	// Use this for initialization
	void Start () {
		hDoor = GetComponent<Collider2D> ();
	}

	// Update is called once per frame
	void Update () {
		Physics2D.IgnoreLayerCollision (0, 11, ignorePlayer); //Makes the player go thru it for now...
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			var wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			var touchPosition = new Vector2 (wp.x, wp.y);

			if (hDoor == Physics2D.OverlapPoint (touchPosition)) { // Our touch overlaps with our Rock Object
				if (ignorePlayer == true) {
					transform.gameObject.GetComponent<SpriteRenderer> ().maskInteraction = SpriteMaskInteraction.None;
				} else {
					transform.gameObject.GetComponent<SpriteRenderer> ().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
				}
				ignorePlayer = !ignorePlayer; //Allow player to disable first touch, and still be able to
				//turn this off/on/off/on
				Physics2D.IgnoreLayerCollision (0, 11, ignorePlayer);

			}
		}
	}
}