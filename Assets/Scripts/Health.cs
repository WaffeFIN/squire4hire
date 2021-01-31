using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
	public GameObject bloodPrefab;
	public GameObject bloodUI;

    void Start()
    {
        currentHealth = maxHealth;
    }

	void Update()
	{
		if (IsDead()) {
			if (gameObject.tag == "Player") {
				var randomIndex = (int) Mathf.Ceil(Random.Range(0.0f, 2.0f));
            	FindObjectOfType<AudioManager>().Stop("footsteps");
				FindObjectOfType<AudioManager>().Play("va_squire_death_" + randomIndex);
			}

			// Let's not destroy the one holding our camera
			if (gameObject.tag != "Hero") {
				Destroy(gameObject);
				var imageManager = GetComponent<ImageManager>();
				if (imageManager != null) {
					Destroy(imageManager.image.gameObject);
				}
			}
		}
	}

    public void TakeDamage() {
		TakeDamage(3);
	}

	private void CreateBlood(int size) {
		var bloodObj = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
		var bloodImageObj = new GameObject("BloodImage");
		bloodImageObj.transform.position = transform.position;

        RectTransform trans = bloodImageObj.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.sizeDelta = new Vector2(size, size);

        Image image = bloodImageObj.AddComponent<Image>();
		image.color = new Color(0.9f, 0.1f, 0.1f, 1.0f);

        bloodImageObj.transform.SetParent(bloodUI.transform);
        image.transform.position = bloodObj.transform.position;

        bloodObj.GetComponent<ImageManager>().image = image;

		var bloodComponent = bloodObj.GetComponent<Blood>();
		var rigidbody = GetComponent<Rigidbody2D>();
		if (rigidbody != null) {
			bloodComponent.velocity = rigidbody.velocity * Time.deltaTime;
		}
		bloodComponent.velocity += 10.0f * Random.insideUnitCircle.normalized;
	}

    public void TakeDamage(int itemsLost) {
		var inventory = GetComponent<Inventory>();
		if (itemsLost > 0 && inventory != null && inventory.itemsCarried.Count > 0) {
			while (itemsLost > 0) {
				itemsLost--;
				inventory.LoseRandomItem(1.5f);
			}
		} else {
        	currentHealth--;
			if (bloodPrefab != null) {
				for (int i = 0; i < 4; i++) {
					var size = 4;
					CreateBlood(size);
				}
			}
		}
		if (gameObject.tag == "Player") {
			var randomIndex = ((int) Mathf.Floor(Random.Range(0.0f, 2.0f))) + 1;
			FindObjectOfType<AudioManager>().Play("va_squire_yelp_" + randomIndex);
		}
		if (gameObject.tag == "Hero") {
			var randomIndex = ((int) Mathf.Floor(Random.Range(0.0f, 2.0f))) + 1;
			FindObjectOfType<AudioManager>().Play("va_hero_grunt_" + randomIndex);
		}
    }

    public void RecoverHealth(int amount) {
        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }
}
