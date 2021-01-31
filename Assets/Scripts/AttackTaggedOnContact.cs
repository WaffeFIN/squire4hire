using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTaggedOnContact : MonoBehaviour
{
	public float swingInterval;
	public float damageDelay;
	public string targetTag;
	public int damageItemsLost;
	
	private float swingTimer = 0.0f;
	private GameObject damageTarget = null;


    void Update()
    {
        swingTimer -= Time.deltaTime;
		if (damageTarget != null && swingTimer < damageDelay) {
			FindObjectOfType<HitSystem>().Hit(gameObject, damageTarget, damageItemsLost);
			damageTarget = null;
		}
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == targetTag && swingTimer < 0.0f) {
			swingTimer = swingInterval;
			damageTarget = other.gameObject;
		}
	}
}
