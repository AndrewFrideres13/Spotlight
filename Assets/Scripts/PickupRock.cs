using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRock : MonoBehaviour {
	public GameManager gManager;
	private Collider2D rock;
	void Start () {
		rock = GetComponent<Collider2D>();
	}
	
	void Update () {
		Physics2D.IgnoreLayerCollision(0,8, true);
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && GameManager.NumRocks < 3) {
             var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
             var touchPosition = new Vector2(wp.x, wp.y);
 
             if (rock == Physics2D.OverlapPoint(touchPosition)){ // Our touch overlaps with our Rock Object
				GameManager.NumRocks++;
				rock.gameObject.SetActive(false);
             } 
         }
	}
}