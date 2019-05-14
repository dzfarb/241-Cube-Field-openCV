using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public const float SPAWN_FREQUENCY = 0.2f;
	public const int HORIZONTAL_RANGE = 20;
	public const float FORWARD_OFFSET = 20f;

	private Transform cubeTransform;
	private Renderer cubeRenderer;

	public static void GenerateCube(GameObject prefabCube, Vector3 playerPosition) {
		Instantiate(prefabCube,
					new Vector3(Mathf.Floor(playerPosition.x + Random.Range(-HORIZONTAL_RANGE, HORIZONTAL_RANGE)), 
								0.5f, 
								Mathf.Floor(playerPosition.z + FORWARD_OFFSET)), 
					Quaternion.identity);
	}

	void Start() {
		cubeTransform = GetComponent<Transform>();
		cubeRenderer = GetComponent<Renderer>();
		cubeRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		//change color
	}

	void FixedUpdate() {
		//If a cube is more than 50 units behind the player, destroy it
		if (cubeTransform.position.z < PlayerController.Instance.GetComponent<Transform>().position.z - 20) {
			Destroy(gameObject);
		}
    }

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
	}
}
