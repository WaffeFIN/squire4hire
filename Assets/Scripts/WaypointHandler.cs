using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    public IList<Vector2> waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        waypoints = new List<Vector2>();
        foreach (var waypoint in gameObject.GetComponentsInChildren<Waypoint>())
        {
            waypoints.Add(waypoint.transform.position);
        }
    }
}
