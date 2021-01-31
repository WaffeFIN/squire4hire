using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public ItemSpawner spawner;

    public GameObject imageObject;
	private GameObject complaintObject;

    public enum HeroState {
		Bashing,
		ComplainingAboutDirt
	}

	public GameObject painZonePrefab;

    public float maxArmorPolish;
    public float armorPolish;
    public float polishingSpeedCoefficient = 5.0f;
    public float complaintInterval = 15.2f;

	private HeroState state = HeroState.Bashing;
    private float nextItemSpawn = 4.0f;
    private float complaintTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3();
		var inventory = GetComponent<Inventory>();
		for (int i = 0; i < 6; i++)
        {
			inventory.AddItem(spawner.SpawnItem("pickupArrow", transform));
		}
		inventory.AddItem(spawner.SpawnItem("pickupMace", transform));
		inventory.AddItem(spawner.SpawnItem("pickupMace", transform));
		inventory.AddItem(spawner.SpawnItem("pickupHealthPotion", transform));
		inventory.AddItem(spawner.SpawnItem("pickupHealthPotion", transform));
		inventory.AddItem(spawner.SpawnItem("pickupHealthPotion", transform));
		inventory.AddItem(spawner.SpawnItem("pickupBow", transform));
		inventory.AddItem(spawner.SpawnItem("pickupShield", transform));
		inventory.AddItem(spawner.SpawnItem("pickupArmor", transform));

		complaintObject = imageObject.GetComponentInChildren<Text>().gameObject;
		complaintObject.SetActive(false);

        FindObjectOfType<AudioManager>().Play("music");
    }

    // Update is called once per frame
    void Update()
    {
		if (armorPolish <= 0) {
			complaintObject.SetActive(true);
			state = HeroState.ComplainingAboutDirt;
		} else {
			armorPolish -= Time.deltaTime * 5;
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
                FindObjectOfType<AudioManager>().PlayRandom("va_polish_armor_command_");
            }
			
			if (armorPolish > maxArmorPolish) {
				armorPolish = maxArmorPolish;
				complaintObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("va_squire_moan");
				complaintTimer = 1f;
				state = HeroState.Bashing;
			}
		}

        var dx = -GetComponent<Rigidbody2D>().velocity.x;

        if (Mathf.Abs(dx) > 20.0) {
            complaintObject.transform.localScale = new Vector2(
				-Mathf.Sign(dx) * Mathf.Abs(complaintObject.transform.localScale.x),
				complaintObject.transform.localScale.y
			);
            imageObject.transform.localScale = new Vector2(-Mathf.Sign(dx), 1.0f);
        }
    }

	float GetRotation(Vector2 v2a, Vector2 v2b) {
		var v2 = v2a - v2b;
 		return Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			switch (state) {
				case HeroState.ComplainingAboutDirt:
					if (Input.GetKeyDown(KeyCode.P)) {
						armorPolish += polishingSpeedCoefficient;
                    	FindObjectOfType<AudioManager>().Play("armor_polish");
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
