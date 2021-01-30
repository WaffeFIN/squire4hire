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
				Destroy(image.gameObject);
			}
		}
	}

    public void TakeDamage() {
        currentHealth--;
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
