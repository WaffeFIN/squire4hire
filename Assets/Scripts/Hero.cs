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

	public HeroState state = HeroState.Bashing;
    public float complaintInterval = 1.2f;

    //temporary spawnTimer
    private float nextItemSpawn = 4.0f;
    private float armorPolish = 42.0f;
    private float complaintTimer = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(200, 200);
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
    }
}
