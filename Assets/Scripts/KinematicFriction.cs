using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFriction : MonoBehaviour
{
    public float friction;

    void Update()
    {
        var itemBody = GetComponent<Rigidbody2D>();
        if (itemBody.velocity.magnitude > 0) {
            itemBody.velocity -= friction * itemBody.velocity.normalized;
            if (itemBody.velocity.magnitude < friction) {
                itemBody.velocity = new Vector2();
            }
        }
    }
}
