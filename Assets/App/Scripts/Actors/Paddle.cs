using UnityEngine;

public class Paddle : MonoBehaviour {
	public float speed = 5.0F;

	private new Rigidbody rigidbody;
	private float xAxis = 0.0F;

	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		//get the paddle's velocity
		var velocity = this.rigidbody.velocity;

		//multiply the horizontal velocity by the key input we get from Update()
		velocity.x = this.speed * this.xAxis;

		//update the paddle's velocity with the new horizontal velocity
		this.rigidbody.velocity = velocity;
	}

	private void Update() {
		//when a player presses 'A', 'D', 'LEFT', or 'RIGHT', change our xAxis from 0 to 1
		this.xAxis = Input.GetAxis("Horizontal");
	}
}