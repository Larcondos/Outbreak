using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public int lives, score, level;					// Various self-explanatory variables in the context of the game.

	public Text scoreText, livesText, levelText;	// Various text variables that display the variables to the player.
	public Text startText, pauseText;				// Text to be displayed when the game is first started or paused.
	private bool paused;							// Used to hold a pause.

	// Use this for initialization
	void Start () {
		// Start Paused
		Time.timeScale = 0;
		startText.text = " Use the arrow keys to break the bricks! \n Clear all of the bricks to beat the level! \n Press ESC to pause at any time! \n Press Space to launch your ball! ";
		pauseText.text = "";
		paused = true;
	}

	// Update is called once per frame
	void Update () {
		// Wait to start the game until Enter is pressed.
		if (startText.text != "") {
			if (Input.GetKey (KeyCode.Space)) {
				Time.timeScale = 1;		
				startText.text = "";
				paused = false;
			}
		} else {
			if (!paused && Input.GetKeyDown (KeyCode.Escape)) {
				PauseGame ();
			} 
			else if (paused && Input.GetKeyDown (KeyCode.Escape)) {
				UnPauseGame ();
			}

			if (paused && (Input.GetKey (KeyCode.Q)))
				Application.Quit ();

		}
		// Keeps the player informed to score, level, and their remaining lives at all times.
		// Could be made more efficient by only updating when these variables are actually changed, but I'm not worried about that quite yet.
		levelText.text = "Level: " + level.ToString();
		livesText.text = "Lives: " + lives.ToString();
		scoreText.text = "Score: " + score.ToString();
	}

	// Pauses the game, and handles associated tasks.
	void PauseGame() {
		Time.timeScale = 0;
		pauseText.text = " The Game is paused. \n Press ESC to unpause. \n Press Q to exit game. ";

		paused = true;
	}

	// Unpauses the game, and handles associated tasks.
	void UnPauseGame() {
		Time.timeScale = 1;
		pauseText.text = "";
		paused = false;
	}




	// Allows other scripts to determine when the player has lost a life.
	public void LoseLife() {
		lives--;
	}

	// Allows the bricks to award the player points.
	public void AddScore(int i) {
		score += i;
	}

}
