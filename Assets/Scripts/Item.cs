﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite SpriteRef;
    public int ItemWeight = 1;

	public float pickupTimer = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (pickupTimer >= 0) pickupTimer -= Time.deltaTime;

        var itemBody = GetComponent<Rigidbody2D>();
        if (itemBody.velocity.magnitude > 0) {
            itemBody.velocity -= 15f * itemBody.velocity.normalized;
            if (itemBody.velocity.magnitude < 15f) {
                itemBody.velocity = new Vector2();
            }
        }
    }
}
