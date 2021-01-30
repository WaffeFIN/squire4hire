using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public TargetMovement heroMovement;
    public WaypointHandler waypointHandler;

    public Vector2 currentWaypointTarget;
    private int currentWayPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentWayPointIndex = 0;
        if (waypointHandler.waypoints.Count > 0) {
            SetWaypointTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heroMovement.IsCloseToTargetX() || heroMovement.IsCloseToTargetY()) {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypointHandler.waypoints.Count) {
                currentWayPointIndex = 0;
            }
            SetWaypointTarget();
        }
    }

    private void SetWaypointTarget() {
        var target = waypointHandler.waypoints[currentWayPointIndex];
        currentWaypointTarget = target;
        heroMovement.target = target;
    }

    private void RestoreWaypointTarget() {
        heroMovement.target = currentWaypointTarget;
    }
}
