using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Bounds worldBounds;
    float vertExtent;
    float horzExtent;
    float leftBound;
    float rightBound;
    float bottomBound;
    float topBound;

    void Update () {
        float camX = Mathf.Clamp (player.transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp (player.transform.position.y, bottomBound, topBound);
        Camera.main.transform.position = new Vector3 (camX, camY, -1);
    }

    void Start () {
        foreach (SpriteRenderer spriteBounds in GameObject.Find ("B&WBG").GetComponentsInChildren<SpriteRenderer> ()) {
            worldBounds.Encapsulate (spriteBounds.bounds);
        }
        vertExtent = Camera.main.GetComponent<Camera> ().orthographicSize;
        horzExtent = vertExtent * Camera.main.GetComponent<Camera> ().aspect; //vertExtent * Screen.width / Screen.height;
        leftBound = worldBounds.min.x + horzExtent;
        rightBound = worldBounds.max.x - horzExtent;
        bottomBound = worldBounds.min.y + vertExtent;
        topBound = worldBounds.max.y - vertExtent;
    }
}