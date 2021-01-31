using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite SpriteRef;

    public int weight = 1;

    public string itemName;

	public float pickupTimer = 0.5f;
    

    // Update is called once per frame
    void Update()
    {
		if (pickupTimer >= 0) pickupTimer -= Time.deltaTime;
    }

	void OnDestroy()
	{
		ScoreSystem.itemsLost++;
	}
}
