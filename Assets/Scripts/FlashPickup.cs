using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashPickup : MonoBehaviour {
	public GameObject tabletHint;
	public GameManager gManager;
	private Color colorForTabletHint;
	private Renderer tabletRender;
	void Start () {
		Renderer tempTabletRender = tabletHint.GetComponent<Renderer> ();
		colorForTabletHint = tempTabletRender.material.color;
		colorForTabletHint.a = 0.01f;
		tempTabletRender.material.color = colorForTabletHint;
		tabletRender = tempTabletRender;
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
			gManager.PickedUpFlashlight = true;
			StartCoroutine (FadeTo (tabletRender, 1f, 5f));
			transform.position = new Vector3 (-1000, -1000, -100); //Hide this
			Invoke ("Cleanup", 5f);
		}
	}

	void Cleanup () {
		transform.gameObject.SetActive (false);
	}
}