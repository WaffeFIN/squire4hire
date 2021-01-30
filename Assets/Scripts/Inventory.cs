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

	public void AddItem(GameObject obj) {
        AddItem(obj, int.MinValue);
	}

	public void AddItem(GameObject obj, int weigth) {
        if (Weight() + weigth <= maxWeight) {
			itemsCarried.Add(obj);
			obj.SetActive(false);
			var imageManager = obj.GetComponent<ImageManager>();
			if (imageManager != null)
			{
				imageManager.image.enabled =false;
			}
		}
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
		if (itemsCarried.Count == 0) return;
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

	public void Pointsify(List<GameObject> targetList) {
		if (itemsCarried.Count >= 3)
			ScoreSystem.tripleRetrievals++;
		
		foreach (var item in itemsCarried) {
			var itemComponent = item.GetComponent<Item>();
			if (itemComponent != null) {
				if (itemComponent.ItemWeight == 1) {
					ScoreSystem.otherItemsRetrieved++;
				}
				if (itemComponent.ItemWeight == 2) {
					ScoreSystem.weaponsRetrieved++;
				}
				if (itemComponent.ItemWeight == 3) {
					ScoreSystem.armorPartsRetrieved++;
				}
			}
		}
		targetList.AddRange(itemsCarried);
		itemsCarried.Clear();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
