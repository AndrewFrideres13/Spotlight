using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectSway : MonoBehaviour {
	void Start() {
		startAngle = transform.eulerAngles;  // Get the start position
	}
	Vector3 startAngle;   //Reference to the object's original angle values
	float rotationSpeed = 0.0004f;  //Speed variable used to control the animation
	float rotationOffset = 12f; //Rotate by 20 units
	float finalAngle;  //Keeping track of final angle to keep code cleaner
	void FixedUpdate() {
		finalAngle = startAngle.z + Mathf.Sin(Environment.TickCount*rotationSpeed)*rotationOffset;  //Calculate animation angle
		transform.eulerAngles = new Vector3(startAngle.x, startAngle.y, finalAngle); //Apply new angle to object
	}
}
