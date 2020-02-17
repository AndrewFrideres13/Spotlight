using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelComplete : MonoBehaviour {
	public GameManager gManager;
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			gameObject.SetActive(false);
			GameManager.LevelNumb++; //Increment the level Number and load the next
			Invoke ("transitionLevel", 0.3f);
		}
	}

	void transitionLevel() {
		SceneManager.LoadScene(GameManager.LevelNumb);
	}
}
