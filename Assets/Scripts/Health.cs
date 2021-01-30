using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

	void Update()
	{
		if (IsDead()) {
			Destroy(gameObject);
			var imageManager = GetComponent<ImageManager>();
			if (imageManager != null) {
				Destroy(imageManager.image.gameObject);
			}
		}
	}

    public void TakeDamage() {
		var inventory = GetComponent<Inventory>();
		if (inventory != null && inventory.itemsCarried.Count > 0) {
			inventory.LoseRandomItem(1.5f);
			inventory.LoseRandomItem(1.5f);
			inventory.LoseRandomItem(1.5f);
		} else {
        	currentHealth--;
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
