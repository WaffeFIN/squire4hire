using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    void Update()
    {
		ScoreSystem.distanceMoved += GetComponent<Rigidbody2D>().velocity.magnitude * Time.deltaTime;
    }
	
    void OnTriggerEnter2D(Collider2D col)
    {
		var itemComponent = col.gameObject.GetComponent<Item>();
        if (itemComponent != null && itemComponent.pickupTimer < 0)
        {
			GetComponent<Inventory>().AddItem(col.gameObject, itemComponent.weight);
        }
    }
}
