using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
		var rigidBody = GetComponent<Rigidbody2D>();
		var vel = rigidBody.velocity;
		var dx = 0;
		var dy = 0;
		var acc = 22;
        if (Input.GetKey("up")) {
			dy += acc;
		}
        if (Input.GetKey("down")) {
			dy -= acc;
		}
        if (Input.GetKey("right")) {
			dx += acc;
		}
        if (Input.GetKey("left")) {
			dx -= acc;
		}
		rigidBody.velocity = new Vector2(vel.x + dx, vel.y + dy);
		var maxSpeed = 220.0f;
		if (rigidBody.velocity.magnitude > maxSpeed) {
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}
    }
}
