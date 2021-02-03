using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSystem : MonoBehaviour
{
	public GameObject bloodUI;

    public void Hit(GameObject source, GameObject target)
	{
		Hit(source, target, 3);
	}

    public void Hit(GameObject source, GameObject target, int itemsLost)
	{
		var health = target.GetComponent<Health>();
		if (health != null && health.HasMercy()) {
			if (target.tag == "Player")
				ScoreSystem.hitsDodged++;
			return;
		}

		if (target.tag == "Player") { // TODO refactor
			FindObjectOfType<AudioManager>().PlayRandom("va_squire_yelp_");
		}
		if (target.tag == "Hero") {
			FindObjectOfType<AudioManager>().PlayRandom("va_hero_grunt_");
		}
		var inventory = target.GetComponent<Inventory>();
		if (itemsLost > 0 && inventory != null && inventory.itemsCarried.Count > 0) {
			while (itemsLost > 0) {
				itemsLost--;
				inventory.LoseRandomItem(1.5f);
			}
			if (health != null) {
				health.GiveInvulnerability(health.mercyInterval);
			}
			return;
		}
		if (health != null) {
			health.TakeDamage(1);

			if (health.bloodPrefab != null) {
				for (int i = 0; i < 4; i++) {
					var size = 4;
					CreateBlood(target.transform.position, health.bloodPrefab, size);
				}
			}
		}
    }

	private void CreateBlood(Vector3 position, GameObject bloodPrefab, int size)
	{
		var bloodObj = Instantiate(bloodPrefab, position, Quaternion.identity);
		var bloodImageObj = new GameObject("BloodImage");
		bloodImageObj.transform.position = position;

        RectTransform trans = bloodImageObj.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.sizeDelta = new Vector2(size, size);

        Image image = bloodImageObj.AddComponent<Image>();
		image.color = new Color(0.9f, 0.1f, 0.1f, 1.0f);

        bloodImageObj.transform.SetParent(bloodUI.transform);
        image.transform.position = bloodObj.transform.position;

        bloodObj.GetComponent<ImageLink>().image = image;

		var simpleFriction = bloodObj.GetComponent<SimpleFriction>();
		var rigidbody = GetComponent<Rigidbody2D>();
		if (rigidbody != null) {
			simpleFriction.velocity = rigidbody.velocity * Time.deltaTime;
		}
		simpleFriction.velocity += 10.0f * Random.insideUnitCircle.normalized;
	}
}
