using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour {
	public RockThrow rockThrowingScript;
	public Button UI_FlashBtn;
	public Button UI_FlashBtn_active;
	public Collider2D flashCollider;
	public Button UI_KeyBtn;
	public Button UI_RockBtn1;
	public Button UI_RockBtn2;
	public Button UI_RockBtn3;
	public Button UI_RockBtn1_Glow;
	public Button UI_RockBtn2_Glow;
	public Button UI_RockBtn3_Glow;
	public GameObject flashLight;
	public GameManager gManager;
	void Start () {
		UI_FlashBtn_active.onClick.AddListener (() => buttonCallBack (UI_FlashBtn_active)); //Callback function below
		UI_KeyBtn.onClick.AddListener (() => buttonCallBack (UI_KeyBtn));
		UI_RockBtn1.onClick.AddListener (() => buttonCallBack (UI_RockBtn1));
		UI_RockBtn2.onClick.AddListener (() => buttonCallBack (UI_RockBtn2));
		UI_RockBtn3.onClick.AddListener (() => buttonCallBack (UI_RockBtn3));
	}

	private void buttonCallBack (Button buttonPressed) {
		if (buttonPressed == UI_FlashBtn_active && GameManager.flashlightHasRecharged == true) { //Need to have RayCast target on in order for this to work
			if (flashLight.activeSelf == true) {
				flashLight.SetActive (false);
				flashCollider.enabled = false;
			} else if (flashLight.activeSelf == false) {
				flashLight.SetActive (true);
				flashCollider.enabled = true;
			}
		} else if (buttonPressed == UI_RockBtn1) {
			if (GameManager.NumRocks > 0) {
				UI_RockBtn1.gameObject.SetActive (false);
				UI_RockBtn1_Glow.gameObject.SetActive (true);
				rockThrowingScript.enabled = true;
			}
		} else if (buttonPressed == UI_RockBtn2) {
			if (GameManager.NumRocks > 0) {
				UI_RockBtn2.gameObject.SetActive (false);
				UI_RockBtn2_Glow.gameObject.SetActive (true);
				rockThrowingScript.enabled = true;
			}
		} else if (buttonPressed == UI_RockBtn3) {
			if (GameManager.NumRocks > 0) {
				UI_RockBtn3.gameObject.SetActive (false);
				UI_RockBtn3_Glow.gameObject.SetActive (true);
				rockThrowingScript.enabled = true;
			}
		}

	} //End callback

	void Update () {
		if (rockThrowingScript.enabled == false) {
			UI_RockBtn1_Glow.gameObject.SetActive (false);
			UI_RockBtn2_Glow.gameObject.SetActive (false);
			UI_RockBtn3_Glow.gameObject.SetActive (false);
		}

		if (GameManager.flashlightHasRecharged == true && gManager.PickedUpFlashlight == true) { //Flashlight usable, so set glowing one to ready
			UI_FlashBtn_active.gameObject.SetActive (true);
			UI_FlashBtn.gameObject.SetActive (false);
		} else if (GameManager.flashlightHasRecharged == false && gManager.PickedUpFlashlight == true) { //Flashlight burnt out, so set non glowing one to active
			UI_FlashBtn_active.gameObject.SetActive (false);
			UI_FlashBtn.gameObject.SetActive (true);
		}
		UI_FlashBtn.gameObject.SetActive (false);
		UI_KeyBtn.gameObject.SetActive (false);
		UI_RockBtn1.gameObject.SetActive (false);
		UI_RockBtn2.gameObject.SetActive (false);
		UI_RockBtn3.gameObject.SetActive (false);

		/*Ensures toggles work correctly */
		if (gManager.PickedUpFlashlight == true) {
			UI_FlashBtn.gameObject.SetActive (true);
		}
		if (gManager.HaveKey == true) {
			UI_KeyBtn.gameObject.SetActive (true);
		}
		if (GameManager.NumRocks == 1) {
			UI_RockBtn1.gameObject.SetActive (true);
		} else if (GameManager.NumRocks == 2) {
			UI_RockBtn1.gameObject.SetActive (true);
			UI_RockBtn2.gameObject.SetActive (true);
		} else if (GameManager.NumRocks == 3) {
			UI_RockBtn1.gameObject.SetActive (true);
			UI_RockBtn2.gameObject.SetActive (true);
			UI_RockBtn3.gameObject.SetActive (true);
		}
	}
}