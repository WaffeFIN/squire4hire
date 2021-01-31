using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
	public GameObject bloodPrefab;

	void Update()
	{
		if (IsDead()) {
			if (gameObject.tag == "Player") {
            	FindObjectOfType<AudioManager>().Stop("footsteps");
				FindObjectOfType<AudioManager>().PlayRandom("va_squire_death_");
			}

			// Let's not destroy the one holding our camera
			if (gameObject.tag != "Hero") {
				Destroy(gameObject);
			}
		}
	}

    public bool IsDead() {
        return currentHealth <= 0;
    }
}
