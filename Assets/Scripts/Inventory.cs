using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public int maxWeight;
    public List<GameObject> itemsCarried = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public int Weight() {
		int weight = 0;
		foreach (var item in itemsCarried) {
			var itemComponent = item.GetComponent<Item>();
			if (itemComponent != null) {
				weight += itemComponent.ItemWeight;
			}
		}
		return weight;
	}

	public void LoseRandomItem() {
		int lossIndex = (int) Mathf.Floor(Random.Range(0, itemsCarried.Count));
		var lostItem = itemsCarried[lossIndex];
		itemsCarried.RemoveAt(lossIndex);

		var itemComponent = lostItem.GetComponent<Item>();
		if (itemComponent != null) {
			itemComponent.pickupTimer = 0.5f;
		}

		lostItem.SetActive(true);
		var imageManager = lostItem.GetComponent<ImageManager>();
		if (imageManager != null)
		{
			imageManager.image.enabled = true;
		}
		lostItem.transform.position = gameObject.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
