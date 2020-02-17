using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {
	public GameObject severedArm;
	public static bool cancelMove = true;
	private bool dirLeftMovement = false;
	private Rigidbody2D rb;
	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;
	public const float SWIPE_THRESH = 2f;
	public bool facingRight = true; //T by default
	private const float MAX_SPEED = 12f;
	private AudioSource source;
	private bool spawnFlag = true;
	public AudioClip playerJump;
	public GameManager gManager;
	private Animator animController; //Needed to set pHP int, playerMoving bool, jump, death trigger for animations
	void Start () {
		source = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();
		animController = GetComponent<Animator> ();
	}
	void Update () {
		if (rb.velocity.magnitude > MAX_SPEED) { //Ensures we get no sudden acceleration bugs
			rb.velocity = Vector2.ClampMagnitude (rb.velocity, MAX_SPEED);
		}
	}
	void FixedUpdate () {
		animController.SetInteger ("pHP", gManager.playerHealth);
		if (spawnFlag == true) {
			rb.sharedMaterial.bounciness = 0.25f;
			spawnFlag = false;
		}
		if (gManager.playerHealth > 0) { //DO this to prevent any weird death glitches
#if UNITY_ANDROID
			CheckTouchInput ();
#endif

			if (dirLeftMovement == true && gManager.playerIsGrounded == true) { //For constant movement L/R
				rb.velocity = new Vector2 (13, rb.velocity.y);
				animController.SetBool ("playerMoving", true);
			} else if (dirLeftMovement == false && gManager.playerIsGrounded == true) {
				rb.velocity = new Vector2 (-13, rb.velocity.y);
				animController.SetBool ("playerMoving", true);
			}

			if (cancelMove == true) { //Our swipe down for killing movement
				rb.velocity = Vector2.zero;
				animController.SetBool ("playerMoving", false);
			}
		}
	}
	void CheckTouchInput () {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}
			if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended) && (touch.phase != TouchPhase.Stationary)) {
				fingerUpPos = touch.position;
				CheckSwipe ();
			}
		}
	}
	void CheckSwipe () { //This will help us figure out what anims/directions to play later...
		float horizMove = fingerDownPos.x - fingerUpPos.x;
		float vertMove = fingerDownPos.y - fingerUpPos.y;
		if (Mathf.Abs (vertMove) > SWIPE_THRESH && Mathf.Abs (vertMove) > Mathf.Abs (horizMove)) {
			if (vertMove > 0) { //Swipe down to cancel Move
				cancelMove = true;
			}

			if (gManager.playerIsGrounded == true && vertMove < 0) { //So nothing goofy happens with jump
				rb.AddForce (new Vector2 (rb.velocity.x * 6.5f, 350.0f), ForceMode2D.Impulse);
				if (!source.isPlaying) {
					animController.SetBool ("playerMoving", false);
					animController.SetTrigger ("jump");
					source.PlayOneShot (playerJump, 1F);
				}
				gManager.playerIsGrounded = false;
				//rb.velocity = Vector2.down*Physics2D.gravity.y*15.0f; //Brings player down quicker..Mario-esque jumping
			}
		} else if (Mathf.Abs (horizMove) > SWIPE_THRESH && Mathf.Abs (horizMove) > Mathf.Abs (vertMove)) {
			if ((horizMove < 0 && !facingRight) || (horizMove > 0 && facingRight)) {
				FlipPlayer ();
			}
			if (horizMove < 0 && rb.velocity.x == 0) { //Setup our bool flags so we can have cont movement in FixedUpdate...
				dirLeftMovement = true;
				cancelMove = false;
			} else if (horizMove > 0 && rb.velocity.x == 0) {
				dirLeftMovement = false;
				cancelMove = false;
			}
		}
	}
	/* Utility stuff for Player below */
	private void FlipPlayer () {
		rb.velocity = Vector2.zero; //Helps movement stay snappy by killing the movement before allowing the player to flip
		facingRight = !facingRight;
		Vector3 pScale = transform.localScale; //Needs to be Vector3 so we keep our Z angle for the player
		Vector3 fScale = severedArm.transform.localScale;
		pScale.x *= -1;
		fScale.y *= -1;
		transform.localScale = pScale; //Essentially toggle everything...-1 flips the x axis
		severedArm.transform.localScale = fScale;
	}

	private void OnApplicationQuit () {
		gManager.flashlightSize = new Vector3 (100, 80, 1);
		gManager.PickedUpFlashlight = false;
		gManager.HaveKey = false;
		GameManager.finalEnemyCount = 2;
		GameManager.ShardNum = 0;
		gManager.playerHealth = 3;
		GameManager.NumRocks = 0;
		GameManager.LevelNumb = 1;
		rb.sharedMaterial.bounciness = 0f;
	}
	public bool GetDirection () {
		return facingRight;
	}
}