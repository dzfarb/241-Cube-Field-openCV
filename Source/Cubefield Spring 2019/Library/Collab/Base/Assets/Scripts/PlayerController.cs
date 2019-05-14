using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	//constants
	public const float BASE_TURN_ACCELERATION = 1f;
	public const float BASE_CAMERA_TURN_ACCELERATION = 0.3f;
	public const float SPEED_UP = 0.0003f;
	public const float DRAG = 0.10f;
	public const float BASE_SPEED = 10f;

	//local fields
	protected static bool alive;
	protected static float horizontalVelocity = 0;
	protected static float forwardVelocity = BASE_SPEED;
	protected static float speedModifier = 1;
	protected static float moveHorizontal;
	protected static Rigidbody rb;
	protected static Transform tf;
	protected static float cameraModifier = 1;

	//editor-modifiable fields
	public GameObject prefabCube;

	//Static singleton property
	public static PlayerController Instance { get; protected set; }

	public float GetSpeedModifier() { 
		return speedModifier;
	}

	public bool IsAlive() {
		return alive;
	}

	public void KillPlayer() {
		OnTriggerEnter(null);
	}

	//instantiate cube at y=0.5 offset randomly in the x up to a maximum of HORIZONTAL_RANGE, FORWARD_OFFSET units in front of the player
	void GenerateCube() {
		Instantiate(prefabCube, 
			new Vector3(tf.position.x + Random.Range(-CubeController.HORIZONTAL_RANGE, CubeController.HORIZONTAL_RANGE), 
				0.5f, 
				tf.position.z + CubeController.FORWARD_OFFSET), 
			Quaternion.identity);
	}

	private void Awake() {
		Instance = this;
	}

	protected void Start() {
        rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
		alive = true;

		if (CameraInputController.cameraInput) {
			CameraInputController.port = 5065;
			CameraInputController.InitUDP();
			cameraModifier = 0.1f;
		} else {
			cameraModifier = 1;
		}
    }
	

    protected void FixedUpdate() {
		//drag to decrease horizontal velocity
		if (Mathf.Abs(horizontalVelocity) > 0.00001f) {
			horizontalVelocity -= DRAG * horizontalVelocity;
		}

		//get input; add it to horizontal velocity
		if (CameraInputController.cameraInput) {
			moveHorizontal = CameraInputController.GetAxisRaw() * BASE_CAMERA_TURN_ACCELERATION;
			//Debug.Log(CameraInputController.GetAxisRaw());
		} else {
			//Debug.Log(Input.GetAxisRaw("Horizontal"));
			moveHorizontal = Input.GetAxisRaw("Horizontal") * BASE_TURN_ACCELERATION;
		}
		horizontalVelocity += moveHorizontal * speedModifier;

		//apply velocity to player
		rb.velocity = new Vector3(horizontalVelocity, 0, forwardVelocity);

		//accelerate over time
		speedModifier += SPEED_UP;
		forwardVelocity = BASE_SPEED * speedModifier * cameraModifier;

		//generate cubes in front of the player
		if (Random.Range(0f, 1f) < CubeController.SPAWN_FREQUENCY) {
			CubeController.GenerateCube(prefabCube, tf.position);
		}
	}

	protected void OnTriggerEnter(Collider other) {
		//kill the player, print the score, then quit
		alive = false;
		HighScoreController.lastScore = ScoreController.GetScore();
		//ScoreController.ResetScore();
		SceneManager.LoadScene("MainMenu");
	}
}
