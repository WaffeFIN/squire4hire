using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	enum TargetType {
		Hero, Both
	}

	Vector3 target;
	float maxSpeed;
	TargetType targets;

    // Update is called once per frame
    void Update()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		var heroRange = 600;

		if (heroes.Length == 1) {
			var newTarget = heroes[0].transform.position;
			if (Vector2.Distance(transform.position, newTarget) < heroRange) {
				GetComponent<TargetMovement>().target = newTarget;
			}
		}
    }
}
