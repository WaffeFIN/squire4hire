using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
	public float acceleration;
	public float maxSpeed;
	public Vector3 target;

    // Update is called once per frame
    void Update()
    {
		var rigidBody = GetComponent<Rigidbody2D>();
		var vel = rigidBody.velocity;
		var dx = target.x - transform.position.x;
		var dy = target.y - transform.position.y;

		var acc = Mathf.Abs(dx) < float.Epsilon && Mathf.Abs(dy) < float.Epsilon ? 0.0f : acceleration / Mathf.Sqrt(dx * dx + dy * dy);
		dx *= acc;
		dy *= acc;

		rigidBody.velocity = new Vector2(vel.x + dx, vel.y + dy);
		
		if (rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}
    }
}
