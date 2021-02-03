using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
	public GameObject bloodPrefab;
    public float mercyInterval;

    public int currentHealth { get; private set; }
    private float mercyTimer;
	private Image associatedImage;

	void Start() {
		currentHealth = maxHealth;

		var imageLink = GetComponent<ImageLink>(); // we're assuming all with Health has an ImageLink
		associatedImage = imageLink.image;
	}

	private static float blinkInterval = 0.2f;

	public bool HasMercy()
	{
		return mercyTimer > 0;
	}

	public void GiveInvulnerability(float seconds)
	{
		mercyTimer = Mathf.Max(seconds, mercyTimer);
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		mercyTimer = mercyInterval;
	}

	void Update()
	{
		mercyTimer -= Time.deltaTime;

		//Mercy invulnerability blinking
        var imageColor = associatedImage.color;
        imageColor.a = mercyTimer < 0 ? 1.0f : 1.0f - (Mathf.Ceil(mercyTimer / blinkInterval) % 2) * 0.5f;
		associatedImage.color = imageColor;

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
