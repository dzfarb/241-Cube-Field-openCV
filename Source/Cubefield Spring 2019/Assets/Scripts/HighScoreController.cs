using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

	private static int highScore = 0;
	private static Text highScoreText;
	public static int lastScore = 0;

    public static void UpdateHighScore(int newScore) {
		if (newScore > highScore) {
			highScore = newScore;
		}
		highScoreText.text = "Top Score:\n" + highScore + "\nLast Score:\n" + lastScore;
	}

	private void Start() {
		highScoreText = GetComponent<Text>();
		UpdateHighScore(lastScore);
	}
}
