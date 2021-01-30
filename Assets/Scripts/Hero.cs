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
    public float complaintInterval = 1.2;

    //temporary spawnTimer
    private float nextItemSpawn = 4;
    private float armorPolish = 42;
    private float complaintTimer = 1.2;

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
			armorPolish -= Time.timeDelta;
		}
		if (Time.time > nextItemSpawn) {
			spawner.SpawnItem("arrow-1", transform);
			var spawnTime = Random.Range(1.2f, 3.0f);
			nextItemSpawn += spawnTime * spawnTime;
		}
    }
}
