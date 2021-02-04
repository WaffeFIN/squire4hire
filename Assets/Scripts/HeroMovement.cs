using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private TargetMovement movement;
    private ApproachInRange approach;

    public WaypointHandler waypointHandler;
    public bool moving = true;

    public Vector2 currentWaypointTarget;
    private int currentWayPointIndex;

    public float fatigue = 0f;

	private enum MoveTowards {
		Enemy,
		Waypoint
	}

	private MoveTowards moveTowards = MoveTowards.Waypoint;

    // Start is called before the first frame update
    void Start()
    {
		movement = GetComponent<TargetMovement>();
		approach = GetComponent<ApproachInRange>();

        currentWayPointIndex = 0;
        if (waypointHandler.waypoints.Count > 0) {
            currentWaypointTarget = waypointHandler.waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
		switch (moveTowards) {
			case MoveTowards.Enemy:
				fatigue += Time.deltaTime;
				if (fatigue > 10f) {
					ChangeMoveTowards(MoveTowards.Waypoint);
				}
				break;
			case MoveTowards.Waypoint:
				if (movement.IsCloseToTarget()) {
					NextWaypointTarget();
				}
				fatigue -= Time.deltaTime;
				if (fatigue < 0) {
					ChangeMoveTowards(MoveTowards.Enemy);
				}
				break;
		}
    }

	private void ChangeMoveTowards(MoveTowards nextTowards)
	{
		if (nextTowards == moveTowards) return;

		switch (moveTowards) {
			case MoveTowards.Enemy:
				approach.enabled = false;
				break;
			case MoveTowards.Waypoint:
				// Do nothing
				break;
		}
		switch (nextTowards) {
			case MoveTowards.Enemy:
				approach.enabled = true;
				approach.alternateTarget = currentWaypointTarget;
				break;
			case MoveTowards.Waypoint:
				RestoreWaypointTarget();
				break;
		}
		moveTowards = nextTowards;
	}

    private void NextWaypointTarget()
	{
		currentWayPointIndex = currentWayPointIndex == waypointHandler.waypoints.Count - 1 ? 0 : currentWayPointIndex + 1;
        var target = waypointHandler.waypoints[currentWayPointIndex];
        currentWaypointTarget = target;
        movement.target = target;
    }

    private void RestoreWaypointTarget()
	{
        movement.target = currentWaypointTarget;
    }
}
