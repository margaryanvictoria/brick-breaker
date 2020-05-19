using UnityEngine;

public class Ball : MonoBehaviour {
	public float startForce = 15.0F;

	private new Rigidbody rigidbody;
	private Vector3 lastVelocity;

	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}

	private void Start() {
		//on start, give the ball a push (z=1)*(startingForce that we chose)
		this.rigidbody.AddForce(Vector3.forward * this.startForce, ForceMode.Force);
		this.lastVelocity = this.rigidbody.velocity;
	}

	private void FixedUpdate() {
		//get the ball's velocity
		var velocity = this.rigidbody.velocity;
		//set the speed equal to the length of the velocity vector (square root x^2+z^2)
		var speed = velocity.magnitude;

		//don't let the ball slow down past 15 or speed up past 20
		speed = Mathf.Clamp(speed, 15.0F, 20.0F);
		//set the ball's new velocity to a length of 1 and multiply it by the new, 15-20f speed
		this.rigidbody.velocity = velocity.normalized * speed;
		//update the last velocity with the ball's previous velocity
		this.lastVelocity = velocity;
	}

	private void OnCollisionEnter(Collision collision) {
		var velocity = this.rigidbody.velocity;

		//velocity of the ball - last velocity == the change in x&z 
		var xDiff = Mathf.Abs(velocity.x - this.lastVelocity.x);
		var zDiff = Mathf.Abs(velocity.z - this.lastVelocity.z);
		
		// possible stuck phase...
		//if there's practically no change in the x or z's distance values
		if(xDiff < 0.1F || zDiff < 0.1F) {
			//calculate a random angle from .01-1.5
			var angle = Random.Range(0.01F, 1.5F);
			//shift the ball's trajectory a bit in the y direction, so it won't forever bounce up/down(z), left/right(x)
			//then when the ball collides with a wall next, it'll have a slightly different angle
			//draw a graph to understand this better :)
			lastVelocity = Quaternion.Euler(0.0F, angle, 0.0F) * lastVelocity;
		}

		//100% elastic collision - no net loss in kinetic energy when it bounces off a wall
		//this is the vector for the path of the ball once it hits something
		var reflection = Vector3.Reflect(this.lastVelocity.normalized, collision.GetContact(0).normal);
		//set the new velocity to the reflected vector, times the ball's speed
		this.rigidbody.velocity = reflection * velocity.magnitude;
	}
}
