using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour {
	public GameObject Wall;
	public GameObject Ground;
	public GameObject Player;
	public GameObject PrefabCube;

	private Renderer WallRenderer;
	private Renderer GroundRenderer;
	private Renderer PlayerRenderer;
	private Renderer CubeRenderer;

	public void Start() {
		WallRenderer = Wall.GetComponent<Renderer>();
		GroundRenderer = Ground.GetComponent<Renderer>();
		PlayerRenderer = Player.GetComponent<Renderer>();
		CubeRenderer = PrefabCube.GetComponent<Renderer>();
	}

	public void Update() {
		if (ScoreController.GetScore() > 5000) {
			//TODO: theme change
		}
	}
}
