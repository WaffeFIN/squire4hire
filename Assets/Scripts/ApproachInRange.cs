using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachInRange : MonoBehaviour
{
	public string targetTag;
	public float range;
	public Vector2? alternateTarget;

	private float updateInterval = 0.4f;
	private float updateTime = 0.4f;

    void Update()
    {
		if (updateTime > 0) {
			updateTime -= Time.deltaTime;
			return;
		}
		updateTime += updateInterval;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

		var target = GetClosestPositionInRange(targets);
		if (target.HasValue || alternateTarget.HasValue) {
			GetComponent<TargetMovement>().target = target.HasValue ? target.Value : alternateTarget.Value;
		}
    }

    private Vector2? GetClosestPositionInRange(GameObject[] targets) {
		if (targets.Length == 0) return null;

        float closestDistance = float.MaxValue;
        Vector2? closestPos = null;
        foreach (var target in targets)
        {
            var targetPos = target.transform.position;
            var distance = Vector2.Distance(transform.position, targetPos);
            if (distance < closestDistance) {
				closestDistance = distance;
                closestPos = targetPos;
                break;
            }
        }
        return Vector2.Distance(transform.position, (Vector3) closestPos) > range ? null : closestPos;
    }
}
