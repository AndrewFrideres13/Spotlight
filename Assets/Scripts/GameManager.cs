using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public static int LevelNumb = 1;
    public static int NumRocks = 0;
    public int playerHealth = 3;
    public static int ShardNum { get; set; } // 5 shards total
    public static int finalEnemyCount = 2;
    public bool HaveKey { get; set; }
    public bool PickedUpFlashlight = false;
    public bool playerIsGrounded { get; set; }
    public bool triggerEnd = false;
    public static bool flashlightHasRecharged = true;
    public Vector3 flashlightSize = new Vector3 (100, 80, 1);

    private AudioSource audioSource;
    public AudioClip[] audioClips;

    void Awake () { // 2 for now since mansion is our current level to work on
        audioSource = transform.GetComponent<AudioSource> ();
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
            //If instance already exists and it's not this:
        } else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy (gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad (gameObject);
        InvokeRepeating ("Ambience", 2f, 30f);
    }

    void Ambience () {
        audioSource.clip = audioClips[Random.Range (0, audioClips.Length)];
        audioSource.Play ();
    }
}