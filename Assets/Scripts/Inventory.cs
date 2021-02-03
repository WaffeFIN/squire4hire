using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public int maxWeight;
    public List<GameObject> itemsCarried = new List<GameObject>();

	public void AddItem(GameObject obj) {
        AddItem(obj, int.MinValue);
	}

	public void AddItem(GameObject obj, int weigth) {
        if (Weight() + weigth <= maxWeight) {
			if (gameObject.tag == "Player") {
				FindObjectOfType<AudioManager>().Play("item_pickup");
			}
			itemsCarried.Add(obj);
			obj.SetActive(false);
			var imageManager = obj.GetComponent<ImageLink>();
			if (imageManager != null)
			{
				imageManager.image.enabled = false;
			}
		}
	}

	public int Weight() {
		int weight = 0;
		foreach (var item in itemsCarried) {
			var itemComponent = item.GetComponent<Item>();
			if (itemComponent != null) {
				weight += itemComponent.weight;
			}
		}
		return weight;
	}

	public void LoseRandomItem() {
		LoseRandomItem(0.5f);
	}

	public void LoseRandomItem(float pickupTimer) {
		if (itemsCarried.Count == 0) return;
		int lossIndex = (int) Mathf.Floor(Random.Range(0, itemsCarried.Count));
		var lostItem = itemsCarried[lossIndex];
		itemsCarried.RemoveAt(lossIndex);

		var itemComponent = lostItem.GetComponent<Item>();
		if (itemComponent != null) {
			itemComponent.pickupTimer = pickupTimer;
		}

		lostItem.SetActive(true);
		var imageManager = lostItem.GetComponent<ImageLink>();
		if (imageManager != null)
		{
			imageManager.image.enabled = true;
		}
		lostItem.transform.position = gameObject.transform.position;
		var velocity = Random.Range(220.0f, 320.0f) * Random.insideUnitCircle.normalized;
		var rigidbody2D = GetComponent<Rigidbody2D>();
		if (rigidbody2D != null) {
			velocity += rigidbody2D.velocity;
		}
		lostItem.GetComponent<Rigidbody2D>().velocity = velocity;
	}

	public bool IsFull() {
		return Weight() >= maxWeight;
	}

	public float GetEncumberance() {
		var ratioFilled = ((float) Weight()) / maxWeight;
		return ratioFilled * ratioFilled;
		//25% full gives 6% encumberance
		//50% full gives 25% encumberance
	}

	public void Pointsify(List<GameObject> targetList) {
		FindObjectOfType<AudioManager>().Play("get_points");
		if (itemsCarried.Count >= 3)
			ScoreSystem.tripleRetrievals++;
		
		foreach (var item in itemsCarried) {
			var itemComponent = item.GetComponent<Item>();
			if (itemComponent != null) {
				if (itemComponent.weight == 1) {
					ScoreSystem.otherItemsRetrieved++;
				}
				if (itemComponent.weight == 2) {
					ScoreSystem.weaponsRetrieved++;
				}
				if (itemComponent.weight == 3) {
					ScoreSystem.armorPartsRetrieved++;
				}
			}
		}
		targetList.AddRange(itemsCarried);
		itemsCarried.Clear();
	}

}
