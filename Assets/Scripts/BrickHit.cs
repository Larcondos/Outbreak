using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHit : MonoBehaviour {

	public int HP;						// The Hit Points of the brick. Varies by type.
	public int pointValue;				// The point value of the brick. Varies by type.
	private int colorIndex;				// Index for holding colors.
	public Color[] Colors;				// Array of Colors

	public AudioSource Hit, Break;		// Sound files for bricks being hit or broken.

	public Controller C;

	// TODO - Needs sound effects and particle effects.

	// Use this for initialization
	void Start () {
		// Will have sounds and particle effects initilization here.
		colorIndex = (HP - 1);
		print (colorIndex);
	}

	// Update is called once per frame
	void Update () {
		// Verify the brick has hit points remaining. If not, the brick is destroyed.
		if (HP <= 0)
			KillMe ();	
		this.GetComponent<Renderer> ().material.color = Colors [colorIndex];
	}

	// Destroys the brick. Called when the brick loses all HP, and gives the player points based on it's brick type.
	void KillMe() {
		GiveScore (pointValue);
		Destroy (this.gameObject);
	}

	// Called when hit by other objects.
	void OnCollisionEnter2D(Collision2D other) {
		// Only called when the ball collides with it. 
		if (other.collider.tag == "Ball") {
			LowerHP ();
		}
	}

	// Lower the brick's HP.
	void LowerHP () {
		HP--;
		if (HP != 0)
			colorIndex--;
	}

	// Give the player Score.
	void GiveScore(int i) {
		C.AddScore (pointValue);
	}

}
