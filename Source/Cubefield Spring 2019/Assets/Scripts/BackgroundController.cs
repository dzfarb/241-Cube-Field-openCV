using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	private Vector3 groundPosition;
	private Vector3 wallPosition;
	private Transform groundTransform;
	private Transform wallTransform;

	//assigned in editor
	public GameObject wall;
	public GameObject ground;

	private void Start() {
		groundTransform = ground.GetComponent<Transform>();
		wallTransform = wall.GetComponent<Transform>();
	}

	private void Update() {

		//move the background so that the player is always in the center
		
		groundPosition = PlayerController.Instance.GetComponent<Transform>().position;
		wallPosition = groundPosition;
		groundPosition.y = 0;
		wallPosition.y = 50;
		wallPosition.z += 50;

		groundTransform.position = groundPosition;
		wallTransform.position = wallPosition;

	}
}
