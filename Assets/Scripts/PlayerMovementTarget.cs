using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTarget : MonoBehaviour
{
	public float dodging = 0.0f;
	public float dodgeSpeedMultiplier;
	public float dodgeTime; // dodge distance is limited by dx/dy (maxSpeed)
	public float dodgeRecoverTime; // when unencumbered
	public float diveRecoverTime; // when encumbered, longer
	public float maxSpeed;
	public float acceleration;

	public bool encumbered = false; // when carrying things

    // Update is called once per frame
    void Update()
    {
		dodging -= Time.deltaTime;
		var dx = 0.0f;
		var dy = 0.0f;
		if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
			dy += maxSpeed;
		}
		if (Input.GetKey("down") || Input.GetKey(KeyCode.S)) 
        {
            dy -= maxSpeed;
		}
		if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
			dx += maxSpeed;
		}
		if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
			dx -= maxSpeed;
		}

        if ((dx != 0.0f || dy != 0.0f) && Input.GetKeyDown("space") && dodging < -diveRecoverTime) {
			dodging = dodgeTime;
		}
		var targetMovement = GetComponent<TargetMovement>();
		if (dodging > 0) {
			targetMovement.maxSpeed = maxSpeed * dodgeSpeedMultiplier;
			targetMovement.acceleration = acceleration * dodgeSpeedMultiplier;
		} else {
			targetMovement.maxSpeed = maxSpeed;
			targetMovement.acceleration = acceleration;
			if ((encumbered && dodging < -diveRecoverTime) || (!encumbered && dodging < -dodgeRecoverTime)) {
				targetMovement.target = new Vector2(transform.position.x + dx, transform.position.y + dy);
			}
		}
    }
}
