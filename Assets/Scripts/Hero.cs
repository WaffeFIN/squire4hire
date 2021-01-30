using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public ItemSpawner spawner;

	public enum HeroState {
		Bashing,
		ComplainingAboutDirt
	}

    public float maxArmorPolish;
    public float armorPolish;
    public float polishingSpeedCoefficient = 10.0f;
    public float complaintInterval = 5.2f;

    //temporary spawnTimer
	private HeroState state = HeroState.Bashing;
    private float nextItemSpawn = 4.0f;
    private float complaintTimer = 5.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
		if (armorPolish <= 0) {
			state = HeroState.ComplainingAboutDirt;
		} else {
			armorPolish -= Time.deltaTime;
		}
		if (Time.time > nextItemSpawn) {
			spawner.SpawnItem("arrow-1", transform);
			var spawnTime = Random.Range(1.2f, 3.0f);
			nextItemSpawn += spawnTime * spawnTime;
		}
		if (state == HeroState.ComplainingAboutDirt) {
			complaintTimer -= Time.deltaTime;
			if (complaintTimer < 0) {
				complaintTimer += complaintInterval;
				//TODO Complain
				print("Complaint!");
			}
		}
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && state == HeroState.ComplainingAboutDirt) {
			armorPolish += Time.deltaTime * polishingSpeedCoefficient;
			if (armorPolish > maxArmorPolish) {
				armorPolish = maxArmorPolish;
				state = HeroState.Bashing;
			}
		}
	}
}
