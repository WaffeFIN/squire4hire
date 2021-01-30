using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public ItemSpawner spawner;

    //temporary spawnTimer
    private float nextSpawn = 4;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(200, 200);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn) {
            spawner.SpawnItem("arrow-1", transform);
            nextSpawn += 4;
        }
    }
}
