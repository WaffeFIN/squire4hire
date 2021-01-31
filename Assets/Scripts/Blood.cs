using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
	public float decay;
	public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        decay = Random.Range(0.6f, 1.4f);
		decay *= decay;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);

		var friction = 12.0f;
        if (velocity.magnitude > 0) {
            velocity -= friction * velocity.normalized * Time.deltaTime;
            if (velocity.magnitude < friction) {
                velocity = new Vector2();
            }
        }

        decay -= Time.deltaTime;
		if (decay < 0)
			Destroy(gameObject);
    }
}
