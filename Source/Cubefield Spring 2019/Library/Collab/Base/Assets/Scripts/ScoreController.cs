using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	const int SCORE_MULTIPLIER = 5;
	public static int score = 0;
	private static Text scoreText;

	public int GetScore() {
		return score;
	}

	public void ResetScore() {
		score = 0;
	}

	private void Start() {
		scoreText = GetComponent<Text>();
	}

	private void FixedUpdate() {
		if (PlayerController.Instance.IsAlive()) {
			score += SCORE_MULTIPLIER;
			scoreText.text = "Score: " + score;
		}
	}
}
