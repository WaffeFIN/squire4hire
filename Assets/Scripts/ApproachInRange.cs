using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachInRange : MonoBehaviour
{
	public enum TargetType {
		Hero, Both
	}

	public TargetType targets;
	public float range;

    // Update is called once per frame
    void Update()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		if (heroes.Length == 1) {
			var newTarget = heroes[0].transform.position;
			var distance = Vector2.Distance(transform.position, newTarget);
			if (distance < range) {
				GetComponent<TargetMovement>().target = newTarget;
			}
		}
    }
}
