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
	public float swingInterval = 1.0f;

    //temporary spawnTimer
	private HeroState state = HeroState.Bashing;
    private float nextItemSpawn = 4.0f;
    private float complaintTimer = 0.0f;
	private float swingTimer = 1.0f;
	private float? swingTowards = null;

    // Start is called before the first frame update
    void Start()
    {
        List<string> inventoryContent = new List<string>()
            {
                "arrow-1",
                "mace",
                "potion",
                "shortbow"
            };
        
        transform.position = new Vector3();
		var inventory = GetComponent<Inventory>();
		for (int i = 0; i < 15; i++)
        {
            var inventoryAccessor = (int)Mathf.Ceil(Random.Range(0.0f, 3.0f));

            var randomItem = spawner.SpawnItem(inventoryContent[inventoryAccessor], transform);
			inventory.AddItem(randomItem);
		}

		complaintObject = imageObject.GetComponentInChildren<Text>().gameObject;
		complaintObject.SetActive(false);

        FindObjectOfType<AudioManager>().Play("music");
    }

    // Update is called once per frame
    void Update()
    {

        swingTimer -= Time.deltaTime;

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

				var randomIndex = (int) Mathf.Ceil(Random.Range(0.0f, 2.0f));
                FindObjectOfType<AudioManager>().Play("va_polish_armor_command_" + randomIndex);
            }
			
			if (armorPolish > maxArmorPolish) {
				armorPolish = maxArmorPolish;
				complaintObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("va_squire_moan");
				state = HeroState.Bashing;
			}
		}

		if (swingTowards != null && swingTimer < swingInterval * 0.875)
		{
			var rotation = (float) swingTowards;
			var painZone = Instantiate(painZonePrefab, transform.position, Quaternion.identity);
			painZone.transform.Rotate(0, 0, rotation);

			var painZoneComponent = painZone.GetComponent<PainZone>();
			painZoneComponent.creator = gameObject;
			var knockbackStrength = 250;
			painZoneComponent.knockback = (Vector2)(Quaternion.Euler(0, 0, rotation) * Vector2.right) * knockbackStrength;
			swingTowards = null;

            var animator = imageObject.GetComponent<Animator>();
            animator.SetBool("swinging", false);
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
		} else {
			if (swingTimer < 0) {
                var animator = imageObject.GetComponent<Animator>();
                animator.SetBool("swinging", true);

				var randomIndex = (int) Mathf.Ceil(Random.Range(0.0f, 5.0f));
                FindObjectOfType<AudioManager>().Play("sword_swoosh_" + randomIndex);
                swingTimer = swingInterval;
				swingTowards = GetRotation(other.transform.position, transform.position);
			}
		}
	}
}
