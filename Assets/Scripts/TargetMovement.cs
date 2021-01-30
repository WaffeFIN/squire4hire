using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
	public float acceleration;
	public float maxSpeed;
	public Vector3 target;

	private static float DISPLACEMENT_EPSILON = 32.0f;

    // Update is called once per frame
    void Update()
    {
		var rigidBody = GetComponent<Rigidbody2D>();
		var vel = rigidBody.velocity;
		var dx = target.x - transform.position.x;
		dx = Mathf.Abs(dx) < DISPLACEMENT_EPSILON ? -vel.x : dx;
		var dy = target.y - transform.position.y;
		dy = Mathf.Abs(dy) < DISPLACEMENT_EPSILON ? -vel.y : dy;

		var acc = acceleration / Mathf.Max(1.0f, Mathf.Sqrt(dx * dx + dy * dy));
		dx *= acc;
		dy *= acc;

		rigidBody.velocity = new Vector2(vel.x + dx, vel.y + dy);
		
		if (rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}
    }

	public bool IsCloseToTargetX() {
		var dx = target.x - transform.position.x;
		return Mathf.Abs(dx) < DISPLACEMENT_EPSILON;
	}

	public bool IsCloseToTargetY() {
		var dy = target.y - transform.position.y;
		return Mathf.Abs(dy) < DISPLACEMENT_EPSILON;
	}
}
