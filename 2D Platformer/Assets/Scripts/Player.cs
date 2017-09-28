using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApax = .4f;
	public float accelerationTimeAirborn = 0.2f;
	public float accelerationTimeGrounded = 0.1f;

	float moveSpeed = 6;
	float gravity;
	float jumpVelocity;
	float velocityXSmoothing;
	Vector3 velocity;

	Controller2D controller;

	void Start () {
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApax, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApax;

		print("Gravity: " + gravity + " Jump velocity: " + jumpVelocity);
	}
	
	// Update is called once per frame
	void Update () {

		if(controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		float targetVelocity = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborn);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
