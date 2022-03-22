using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour {
    public float walkSpeed = 1.0f; // Walkspeed
    private float definedWalkSpeed;
    public float wallLeft = 0.0f; // Define wallLeft
    public float wallRight = 5.0f; // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    //float originalX;
    private Rigidbody2D enemy;
    public int enemyHP = 3;
    public AudioClip ghostWail;
    private bool facingRight = false; //F by default
    private Vector3 pScale; //Needs to be Vector3 so we keep our Z angle for the player
    Animator animController;

    void Start () {
        wallLeft = transform.position.x - 7.0f;
        wallRight = transform.position.x + 7.0f;
        enemy = GetComponent<Rigidbody2D> ();
        pScale = transform.localScale;
        definedWalkSpeed = walkSpeed;
        animController = GetComponent<Animator> ();
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if (coll.gameObject.name == "FlashContainer") {
            if (enemyHP > 0) { // Enemy has reached 0 HP so disable them
                AudioSource.PlayClipAtPoint (ghostWail, transform.position);
                enemyHP--; //Lower HP..
                gameObject.GetComponent<Collider2D> ().enabled = false;
                walkSpeed = 0;
                animController.SetBool ("isStunned", true);
                if (transform.gameObject.name == "FinalGhost") {
                    GameManager.finalEnemyCount -= 1;
                }
            } else {
                animController.SetTrigger ("isDying");
                Invoke ("ghostDeath", 1.5f);
            }
            Invoke ("stunGhost", 5.0f); //Call this here..wont be called if ghsot is disabled anyways
        }
    }
    void stunGhost () {
        gameObject.GetComponent<Collider2D> ().enabled = true;
        walkSpeed = definedWalkSpeed;
        animController.SetBool ("isStunned", false);
    }

    void ghostDeath () {
        enemy.gameObject.SetActive (false);
    }

    void FixedUpdate () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
            walkingDirection = -1.0f;
            pScale.x *= -1;
            facingRight = !facingRight;
        } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft) {
            walkingDirection = 1.0f;
            pScale.x *= -1;
            facingRight = !facingRight;
        }
        transform.localScale = pScale; //Essentially toggle everything...-1 flips the x axis
        transform.Translate (walkAmount);
    }
}