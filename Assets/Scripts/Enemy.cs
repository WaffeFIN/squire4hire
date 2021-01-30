using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Sprite SpriteRef;

	public float swingInterval = 1.1f;
	public float damageDelay = 0.5f;
	private float swingTimer = 1.1f;
	private GameObject damageTarget = null;

    void Update()
    {
        swingTimer -= Time.deltaTime;
		if (damageTarget != null && swingTimer < damageDelay) {
			damageTarget.GetComponent<Health>().TakeDamage(1);
			damageTarget = null;
		}
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Hero") {
			swingTimer = swingInterval;
			damageTarget = other.gameObject;
		}
	}
}
