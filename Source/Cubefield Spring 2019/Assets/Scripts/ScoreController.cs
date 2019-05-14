using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	private const int SCORE_MULTIPLIER = 5;
	private static int score = 0;
	private static Text scoreText;

	public static int GetScore() {
		return score;
	}

	private void Start() {
		scoreText = GetComponent<Text>();
		score = 0;
	}

	private void FixedUpdate() {
		if (PlayerController.Instance.IsAlive()) {
			score += SCORE_MULTIPLIER;
			scoreText.text = "Score: " + score;
		}
	}
}
