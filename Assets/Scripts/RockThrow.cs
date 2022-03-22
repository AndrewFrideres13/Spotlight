using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour {
	public GameObject rock;
	public PlayerController playerObj;
	private Vector2 offset;
	private Vector2 leftOffset;
	public AudioClip rockThrowSound;
	public GameManager gManager;
	//private Animator animController; //Needed to set rThrow
	void Start () {
		offset = new Vector2 (15.0f, 20.0f);
		leftOffset = new Vector2 (-15.0f, 20.0f);
	}

	void FixedUpdate () {
		if (Input.touchCount > 0) {
			playerObj.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			if (Input.GetTouch (0).phase == TouchPhase.Began && GameManager.NumRocks > 0) { // Tapped screen
				AudioSource.PlayClipAtPoint (rockThrowSound, transform.position);
				//animController.SetTrigger("rThrow");
				Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				Vector2 dir = touchPos - (new Vector2 (transform.position.x, transform.position.y));
				dir.Normalize ();
				//Makes it ignore everything on default layer so
				//we can set this up to only detect stuff on the rockThrown object layer
				Physics2D.IgnoreLayerCollision (0, 8, true);
				Physics2D.IgnoreLayerCollision (8, 9, true);
				Physics2D.IgnoreLayerCollision (8, 10, true);
				if (playerObj.GetDirection () == true) { //Facing right
					GameObject tossedRock = (GameObject) (Instantiate (rock, (Vector2) transform.position + offset * transform.localScale.x, Quaternion.identity));
					tossedRock.SetActive (true);
					tossedRock.GetComponent<Rigidbody2D> ().velocity = dir * 18.0f;
				} else if (playerObj.GetDirection () == false) {
					GameObject tossedRock = (GameObject) (Instantiate (rock, (Vector2) (transform.position) + leftOffset * -transform.localScale.x, Quaternion.identity));
					tossedRock.SetActive (true);
					tossedRock.GetComponent<Rigidbody2D> ().velocity = dir * 18.0f;
				}
				GameManager.NumRocks--;
				this.enabled = false;
			}
		}
	}
}