using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour {

	private Rigidbody2D rb2d;		// Rigibody2D component attached to the paddle.
	public int speed;				// Speed at which the paddle moves.
	public GameObject Ball;			// The ball object.
	private BallMove BM;			// Script which controls the ball's movements (apart from physics)
	private Rigidbody2D ballrb2d;	// The Ball's rigidbody2D component.
	private bool sticky;			// Sticky boolean, used at beginning of "life" to keep the ball attached to paddle.

	// Initializing Variables.
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		BM = Ball.GetComponent<BallMove> ();
		ballrb2d = Ball.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame.
	void Update () {
		// Recieves input from player to move horizontally, which is then applied immediately after. GetAxisRaw keeps this movement rigid, and not floaty.
		var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0); 
		rb2d.velocity = move * speed * Time.deltaTime; 

		// Spacebar turns off sticky, and then allows the ball to be launched.
		if (sticky && Input.GetKeyDown (KeyCode.Space)) {
			sticky = false;
			ballrb2d.velocity = new Vector3(1, 1, 0) * BM.speed * Time.deltaTime;
			// Return the ball to it's natural state.
			Ball.transform.parent = null;
			ballrb2d.isKinematic = false;
		}
	}

	// Called when the Paddle collides with other objects.
	void OnCollisionEnter2D(Collision2D other) {
		// Only respondes if it collides with the ball.
		// If the paddle is 'sticky', then the ball will become a child of the paddle and move dependently by the paddle.
		if (other.collider.tag == "Ball") {
			if (sticky) {
				other.collider.transform.SetParent (this.transform);
				ballrb2d.isKinematic = true;
				ballrb2d.velocity = Vector3.zero;
			}
		}
	}

	// Allows the paddle to be set to 'sticky' when necessary.
	public void SetSticky() {
		sticky = true;
	}

}
