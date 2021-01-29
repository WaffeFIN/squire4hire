using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
		var dx = 0.0f;
		var dy = 0.0f;
        if (Input.GetKey("up")) {
			dy += 100;
		}
        if (Input.GetKey("down")) {
			dy -= 100;
		}
        if (Input.GetKey("right")) {
			dx += 100;
		}
        if (Input.GetKey("left")) {
			dx -= 100;
		}
		var targetMovement = GetComponent<TargetMovement>();
		targetMovement.target = new Vector2(transform.position.x + dx, transform.position.y + dy);
    }
}
