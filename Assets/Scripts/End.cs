using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class End : MonoBehaviour {
	public GameObject playerBlendFade;
	public GameObject colorLayer;
	public GameObject bwLayer;
	public GameObject coloredOrb;
	public Image UIFade;
	public Image finalFade;
	public GameObject player;
	public GameManager gManager;
	private Color alphaCol;
	private Renderer playerRend;

	void Start () {
		Renderer tempPRenderer = playerBlendFade.GetComponent<Renderer> ();
		UIFade.canvasRenderer.SetAlpha (0.01f);
		finalFade.canvasRenderer.SetAlpha (0.01f);
		alphaCol = tempPRenderer.material.color;
		alphaCol.a = 0.01f;
		tempPRenderer.material.color = alphaCol;
		playerRend = tempPRenderer;
	}

	IEnumerator FadeTo (Renderer rend, float opacity, float time) {
		Color col = rend.material.color;
		float begOpacity = col.a;

		float i = 0;
		while (i < time) {
			i += Time.deltaTime;
			float blend = Mathf.Clamp01 (i / time);
			col.a = Mathf.Lerp (begOpacity, opacity, blend);
			rend.material.color = col;
			yield return null;
		}
	}
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name == "Player") {
			if (gManager.triggerEnd == true) {
				GameManager.flashlightHasRecharged = false;
				bwLayer.GetComponent<SpriteRenderer> ().maskInteraction = SpriteMaskInteraction.None;
				bwLayer.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				colorLayer.GetComponent<SpriteRenderer> ().maskInteraction = SpriteMaskInteraction.None;
				//We set these both to have no mask interaction..and set the B&W on top
				coloredOrb.SetActive (true);
				StartCoroutine (FadeTo (bwLayer.GetComponent<SpriteRenderer> (), 0f, 5f)); //Now we fade the B&W
				StartCoroutine (FadeTo (coloredOrb.GetComponent<SpriteRenderer> (), 0f, 25f));
				playerBlendFade.SetActive (true);
				UIFade.gameObject.SetActive (true);
				player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll; //Freeze player for end
				StartCoroutine (FadeTo (playerRend, 1f, 20f));
				UIFade.CrossFadeAlpha (1.0f, 20.0f, true); //Fade in a darks creen that blends character in...Hides UI
				Invoke ("endFade", 25f);
			}
		}
	}

	void endFade () {
		finalFade.gameObject.SetActive (true);
		finalFade.CrossFadeAlpha (1.0f, 7.5f, true);
		Invoke ("restart", 8);
	}

	void restart () { //resets below...as a paranoia check
		GameManager.LevelNumb = 1;
		GameManager.ShardNum = 0;
		GameManager.NumRocks = 0;
		gManager.HaveKey = false;
		gManager.playerHealth = 3;
		gManager.flashlightSize = new Vector3 (100, 80, 0);
		SceneManager.LoadScene (0); //Reload Menu
	}
}