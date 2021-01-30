using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainZone : MonoBehaviour
{
	public string targetsOnly;
	public GameObject creator;

	public Vector2 knockback;

	void Start() {
		print("PainZone Pos  " + gameObject.transform.position);
		print("PainZone Rot  " + gameObject.transform.rotation);
	}
	
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject != creator && (targetsOnly == null || targetsOnly == "" || targetsOnly == other.tag)) {
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

	void LateUpdate() {
		//Destroy(gameObject);
	}
}
