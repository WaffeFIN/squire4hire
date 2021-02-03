using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTarget : MonoBehaviour
{

	public GameObject imageObject;
	public float dodging = 0.0f;
	public float dodgeSpeedMultiplier;
	public float dodgeTime; // dodge distance is limited by dx/dy (maxSpeed)
	public float dodgeRecoverTime; // when unencumbered
	public float diveRecoverTime; // when encumbered, longer
	public float maxSpeed;
	public float acceleration;


	private float encumberance = 0.0f;
    public bool isMoving = false;
    public bool wasMoving = true;

	(float, float) GetMovement() {
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
		return (dx, dy);
	}


    // Update is called once per frame
    void Update()
    {
		dodging -= Time.deltaTime;
		var (dx, dy) = GetMovement();
        wasMoving = isMoving;
        isMoving = dx != 0.0f || dy != 0.0f;

		encumberance = GetComponent<Inventory>().GetEncumberance();

        if (!wasMoving && isMoving)
        {
            FindObjectOfType<AudioManager>().Play("footsteps");
        }
        if (wasMoving && !isMoving)
        {
            FindObjectOfType<AudioManager>().Stop("footsteps");
        }

        var animator = imageObject.GetComponent<Animator>();
        animator.SetBool("moving", isMoving);

        if (dx != 0)
        {
            imageObject.transform.localScale = new Vector2(-Mathf.Sign(dx), 1.0f);
        }
        if (isMoving && Input.GetKeyDown("space") && dodging < -diveRecoverTime) {
			dodging = dodgeTime;
			GetComponent<Health>().GiveInvulnerability(dodgeTime);
			if (Random.Range(0.0f, 1.0f) < ChanceOfItemLoss())
			{
				GetComponent<Inventory>().LoseRandomItem();
			}
            FindObjectOfType<AudioManager>().Play("dash");
		}
		
		var targetMovement = GetComponent<TargetMovement>();
		if (dodging > 0) {
			targetMovement.maxSpeed = maxSpeed * dodgeSpeedMultiplier;
			targetMovement.acceleration = acceleration * dodgeSpeedMultiplier;
		} else {
			targetMovement.maxSpeed = maxSpeed;
			targetMovement.acceleration = acceleration;
			if (dodging < -dodgeRecoverTime * (1.0f - encumberance) - diveRecoverTime * encumberance) {
				targetMovement.target = new Vector2(transform.position.x + dx, transform.position.y + dy);
			}
		}
    }

	private float ChanceOfItemLoss()
	{
		return Mathf.Max(0.0f, Mathf.Min(1.0f, encumberance - 0.5f));
	}
}
