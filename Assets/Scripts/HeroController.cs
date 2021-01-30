using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public TargetMovement heroMovement;
    public WaypointHandler waypointHandler;
    public bool moving = true;

    public Vector2 currentWaypointTarget;
    private int currentWayPointIndex;

    public float aggroRange = 100;
    public float fatigue = 0f;
    public bool coolingDown = false;

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
		if (!coolingDown) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0) {
                var closestEnemyInRange = GetClosestEnemyInAggroRange(enemies);
                if (closestEnemyInRange.HasValue) {
                    heroMovement.target = closestEnemyInRange.Value;
                    fatigue += Time.deltaTime;
                    if (fatigue > 10f) {
                        coolingDown = true;
                    }
                    return;
                }
		    }
        }

        if (heroMovement.IsCloseToTargetX() || heroMovement.IsCloseToTargetY()) {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypointHandler.waypoints.Count) {
                currentWayPointIndex = 0;
            }
            SetWaypointTarget();
        } else {
            RestoreWaypointTarget();
        }
        
        if (fatigue > 0) {
            fatigue -= Time.deltaTime;
        } else {
            fatigue = 0;
            coolingDown = false;
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

    private Vector3? GetClosestEnemyInAggroRange(GameObject[] enemies) {
        int? closestDistance = null;
        Vector3? closestTarget = null;
        foreach (var enemy in enemies)
        {
            var newTarget = enemy.transform.position;
            var distance = Vector2.Distance(transform.position, newTarget);
            if (distance < aggroRange && (!closestDistance.HasValue || distance < closestDistance)) {
                closestTarget = newTarget;
                break;
            }
        }
        return closestTarget;
    }
}
