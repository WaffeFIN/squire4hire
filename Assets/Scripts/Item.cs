using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite SpriteRef;
    public int ItemWeight = 1;

    private string itemName = "Arrow";

	public float pickupTimer = 0.5f;

	private float decay = 30.0f;
    

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

		decay -= Time.deltaTime;
		if (decay < 0.0f) Destroy(gameObject);
    }

    public string GetName() {
        return itemName;
    }
}
