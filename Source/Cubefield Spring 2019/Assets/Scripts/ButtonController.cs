using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public GameObject MainMenu;

	public void StartGame() {
		SceneManager.LoadScene("Cubefield");
	}
}
