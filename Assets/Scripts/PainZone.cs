using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainZone : MonoBehaviour
{
	public string targetsOnly;
	public GameObject creator;

	public Vector2 knockback;

	private int ticks = 5;

	private List<GameObject> hits = new List<GameObject>();

	void Update() {
		if (ticks <= 0)
			Destroy(gameObject);
		ticks--;
	}
	
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject != creator && (targetsOnly == null || targetsOnly == "" || targetsOnly == other.tag)) {
			if (other.tag == "Player") {
				if (other.GetComponent<PlayerMovementTarget>().dodging >= 0) {
					ScoreSystem.hitsDodged++;
					hits.Add(other.gameObject); //ignore player
					return;
				}
			}
			if (!hits.Contains(other.gameObject)) {
				hits.Add(other.gameObject);
				var health = other.GetComponent<Health>();
				if (health != null) {
					health.TakeDamage();
				}
				var rigidbody2d = other.GetComponent<Rigidbody2D>();
				if (rigidbody2d != null) {
					rigidbody2d.velocity += knockback;
				}
			}
		}
	}
	
}
