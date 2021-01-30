using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public ItemSpawner spawner;

	public enum HeroState {
		Bashing,
		ComplainingAboutDirt
	}

    public float maxArmorPolish;
    public float armorPolish;
    public float polishingSpeedCoefficient = 10.0f;
    public float complaintInterval = 5.2f;

    //temporary spawnTimer
	private HeroState state = HeroState.Bashing;
    private float nextItemSpawn = 4.0f;
    private float complaintTimer = 5.2f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(200, 200);
		var inventory = GetComponent<Inventory>();
		for (int i = 0; i < 15; i++) {
			var arrow = spawner.SpawnItem("arrow-1", transform);
			inventory.AddItem(arrow);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (armorPolish <= 0) {
			state = HeroState.ComplainingAboutDirt;
		} else {
			armorPolish -= Time.deltaTime;
		}
		if (Time.time > nextItemSpawn) {
			GetComponent<Inventory>().LoseRandomItem();
			var spawnTime = Random.Range(1.2f, 3.0f);
			nextItemSpawn += spawnTime * spawnTime;
		}
		if (state == HeroState.ComplainingAboutDirt) {
			complaintTimer -= Time.deltaTime;
			if (complaintTimer < 0) {
				complaintTimer += complaintInterval;
				//TODO Complain
				print("Complaint!");
			}
		}
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			switch (state) {
				case HeroState.ComplainingAboutDirt:
					armorPolish += Time.deltaTime * polishingSpeedCoefficient;
					if (armorPolish > maxArmorPolish) {
						armorPolish = maxArmorPolish;
						state = HeroState.Bashing;
					}
					break;
				case HeroState.Bashing:
					var playerInventory = other.gameObject.GetComponent<Inventory>();
					if (playerInventory.itemsCarried.Count > 0) {
						var heroInventory = GetComponent<Inventory>();
						playerInventory.Pointsify(heroInventory.itemsCarried);
					}
					break;
			}
		}
	}
}
