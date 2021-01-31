using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnContact : MonoBehaviour
{

	public GameObject spawnObject;

	public string contactTag;
	public float spawnInterval;
	public float spawnDelay;
    public bool spawning; 
	public Animator spawnAnimator;

	private float spawnTimer = 1.0f;
	private float? spawnTowards = null;

    void Update()
    {
        spawnTimer -= Time.deltaTime;

		if (spawnTowards != null && spawnTimer < spawnInterval - spawnDelay)
		{
			var rotation = (float) spawnTowards;
			var spawnedObj = Instantiate(spawnObject, transform.position, Quaternion.identity);
			spawnedObj.transform.Rotate(0, 0, rotation);

			var painZone = spawnedObj.GetComponent<PainZone>();
			if (painZone != null) {
				painZone.creator = gameObject;
				var knockbackStrength = 250;
				painZone.knockback = (Vector2)(Quaternion.Euler(0, 0, rotation) * Vector2.right) * knockbackStrength;
			}

            spawning = false;
			if (spawnAnimator != null) {
            	spawnAnimator.SetBool("swinging", false);
			}

			spawnTowards = null;
		}
    }

	float GetRotation(Vector2 v2a, Vector2 v2b) {
		var v2 = v2a - v2b;
 		return Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == contactTag || contactTag == null) {
			if (spawnTimer < 0) {
                spawning = true;
				if (spawnAnimator != null) {
					spawnAnimator.SetBool("swinging", true);
				}

                FindObjectOfType<AudioManager>().PlayRandom("sword_swoosh_");
                spawnTimer = spawnInterval;
				spawnTowards = GetRotation(other.transform.position, transform.position);
			}
		}
	}
}
