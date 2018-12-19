using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

	private Rigidbody2D rb2d;	// Rigidbody2D component attached to the ball.
	public float speed;			// Speed at which the ball moves.
	public Controller C;		// Game Controller script attached to Camera (for sake of access).
	public PaddleMove PM;		// Script for the paddle's movement and it's other functions.

	// Initializing variables and 'sticking' the ball to the paddle.
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		this.gameObject.transform.position = PM.gameObject.transform.position + new Vector3 (0, 0.9f, 0);
		rb2d.velocity = new Vector3(1, 1, 0) * speed * Time.deltaTime;

	}

	// Update is called once per frame - Keep the ball's speed under control.
	void Update () {
		rb2d.velocity = speed * rb2d.velocity.normalized;
	}

	// Called when the ball collides with a trigger.
	void OnTriggerEnter2D(Collider2D other) {
		// Only called when trigger is for going off the allowed screen, or "out of play".
		// The controller is accessed to lose a life, and the ball is reset to it's initial state before it moves like at the beginning of the level.
		if (other.name == "OutofPlay") {
			C.LoseLife ();
			PM.SetSticky ();
			this.gameObject.transform.position = PM.gameObject.transform.position + new Vector3(0,0.9f,0);
			speed = 10;
			//print ("out of play");
		}
	}

	// Called when the ball collides with other objects.
	void OnCollisionEnter2D(Collision2D other) {
		// Only called if speed is less than or equal to 25. Above this speed the game becomes unfair and unstable.
		if (speed <= 25) {
			// Increase the speed by a higher increment for hitting the paddle over walls or bricks.
			if (other.collider.tag == "Paddle") {
				speed += 0.5f;
			} else
				speed += 0.1f;
		}
	}

}
