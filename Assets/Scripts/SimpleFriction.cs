using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFriction : MonoBehaviour
{
	public Vector2 velocity;
	public float friction;

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);

        if (velocity.magnitude > 0) {
            velocity -= friction * velocity.normalized * Time.deltaTime; //TODO: this does not count lag correctly
            if (velocity.magnitude < friction) {
                velocity = new Vector2();
            }
        }
    }
}
