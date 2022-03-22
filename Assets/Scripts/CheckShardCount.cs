using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckShardCount : MonoBehaviour {
	public GameManager gManager;
	private SpriteRenderer playerObj;
	private Animator playerAnim;
	public RuntimeAnimatorController playerAnimControl0;
	public RuntimeAnimatorController playerAnimControl1;
	public RuntimeAnimatorController playerAnimControl2;
	public RuntimeAnimatorController playerAnimControl3;
	public RuntimeAnimatorController playerAnimControl4;
	public RuntimeAnimatorController playerAnimControl5;
	public RuntimeAnimatorController playerAnimControl6;
	public Sprite playerSprite0;
	public Sprite playerSprite1;
	public Sprite playerSprite2;
	public Sprite playerSprite3;
	public Sprite playerSprite4;
	public Sprite playerSprite5;
	public Sprite playerSprite6;
	void Start () {
		playerObj = GetComponent<SpriteRenderer> ();
		playerAnim = GetComponent<Animator> ();
	}
	void checkShardNum () {
		switch (GameManager.ShardNum) {
			case 1:
				playerObj.sprite = playerSprite1;
				playerAnim.runtimeAnimatorController = playerAnimControl1;
				GameManager.ShardNum = 1;
				break;
			case 2:
				playerObj.sprite = playerSprite2;
				playerAnim.runtimeAnimatorController = playerAnimControl2;
				GameManager.ShardNum = 2;
				break;
			case 3:
				playerObj.sprite = playerSprite3;
				playerAnim.runtimeAnimatorController = playerAnimControl3;
				GameManager.ShardNum = 3;
				break;
			case 4:
				playerObj.sprite = playerSprite4;
				playerAnim.runtimeAnimatorController = playerAnimControl4;
				GameManager.ShardNum = 4;
				break;
			case 5:
				playerObj.sprite = playerSprite5;
				playerAnim.runtimeAnimatorController = playerAnimControl5;
				GameManager.ShardNum = 5;
				break;
			case 6:
				playerObj.sprite = playerSprite6;
				playerAnim.runtimeAnimatorController = playerAnimControl6;
				GameManager.ShardNum = 6;
				break;
			default:
				playerObj.sprite = playerSprite0;
				playerAnim.runtimeAnimatorController = playerAnimControl0;
				GameManager.ShardNum = 0;
				break;
		}
	}

	void Update () {
		checkShardNum ();
	}
}