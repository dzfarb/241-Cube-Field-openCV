using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public const float VERTICAL_OFFSET = 2;
	public const float HORIZONTAL_OFFSET = 4;
	public const float CAMERA_OBLIQUITY_MULTIPLIER = 2.5f;

	protected Vector3 position;
	protected Transform cameraTransform;
	protected Transform playerTransform;
	protected Rigidbody playerRigidbody;

	void Start() {
		cameraTransform = GetComponent<Transform>();
		playerTransform = PlayerController.Instance.GetComponent<Transform>();
		playerRigidbody = PlayerController.Instance.GetComponent<Rigidbody>();
	}

	protected void Update() {

		//move camera VERTICAL_OFFSET units above and HORIZONTAL_OFFSET units behind player
		position = playerTransform.position;
		position.y += VERTICAL_OFFSET;
		position.z -= HORIZONTAL_OFFSET;
		cameraTransform.position = position;

		//reset camera rotation
		cameraTransform.eulerAngles = Vector3.zero;
		//rotate around the player on the Z (forward) axis proportional to the horizontal velocity / speed modifier
		cameraTransform.RotateAround(playerTransform.position, 
			Vector3.forward, 
			playerRigidbody.velocity.x * -CAMERA_OBLIQUITY_MULTIPLIER / PlayerController.Instance.GetSpeedModifier());
	}
}