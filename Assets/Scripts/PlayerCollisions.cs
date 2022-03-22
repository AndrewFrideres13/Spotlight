using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCollisions : MonoBehaviour {
	public AudioClip playerHurt;
	public AudioClip playerDeath;
	public AudioClip pickupShard;
	public AudioClip keyPickup;
	public AudioClip gateOpen;
	public GameManager gManager;
	private Animator animController;

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "enemy_ghost") {
			AudioSource.PlayClipAtPoint (playerHurt, transform.position);
			gManager.playerHealth--;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-10, 13), ForceMode2D.Impulse); //Knocks player back super quick
			if (gManager.playerHealth <= 0) { //Chance to play death sound/ANim before we kick off GAME OVER state
				AudioSource.PlayClipAtPoint (playerDeath, transform.position);
				animController.SetTrigger ("death");
				Invoke ("GameOver", 5);
			}
		} else if (coll.gameObject.name == "Shard") {
			coll.gameObject.SetActive (false);
			AudioSource.PlayClipAtPoint (pickupShard, transform.position);
			GameManager.ShardNum++;
		} else if (coll.gameObject.name == "key") {
			AudioSource.PlayClipAtPoint (keyPickup, transform.position);
			coll.gameObject.SetActive (false);
			gManager.HaveKey = true;
			//Add key to inventory
		} else if (coll.gameObject.name == "Gate") {
			if (gManager.HaveKey == true) { //Play unlocking sound of some kind
				AudioSource.PlayClipAtPoint (gateOpen, transform.position);
				coll.gameObject.SetActive (false); //Disables collider, allows passage
				gManager.HaveKey = false;
			}
		} else if (coll.gameObject.name == "WCollisions") {
			transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			PlayerController.cancelMove = true;
		} else if (coll.gameObject.name == "LevelComplete") {
			transform.GetComponent<Rigidbody2D> ().sharedMaterial.bounciness = 0f;
		}
	}

	void OnCollisionStay2D (Collision2D coll) { //Could just set CHandeliers/Door to ground layer but...this is me being explicit
		if (coll.gameObject.tag == "Ground" || coll.gameObject.name == "Chandeliers" || coll.gameObject.name == "HiddenDoor") {
			transform.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1.5F, ForceMode2D.Impulse); //Push back up to counteract the gravity...keeps player from getting stuck at times
			gManager.playerIsGrounded = true;
		}
	}
	void OnCollisionExit2D (Collision2D coll) {
		if (coll.gameObject.tag == "Ground" || coll.gameObject.name == "Chandeliers" || coll.gameObject.name == "HiddenDoor") {
			gManager.playerIsGrounded = false;
		}
	}
	void GameOver () {
		SceneManager.LoadScene (5); //GameOver scene, we save the current level for reloading it later....
	}

	void Update () {
		Physics2D.IgnoreLayerCollision (0, 12, true);
	}
}