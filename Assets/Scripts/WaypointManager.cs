using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform GetNextWaypoint(int currentWaypointIndex)
    {
        if (waypoints.Length == 0) return null;
        return waypoints[(currentWaypointIndex + 1) % waypoints.Length];
    }

    public Transform GetRandomWaypoint()
    {
        if (waypoints.Length == 0) return null;
        return waypoints[Random.Range(0, waypoints.Length)];
    }

    public Vector3 GetWaypointPosition(int index)
    {
        if (index < 0 || index >= waypoints.Length) return Vector3.zero;
        return waypoints[index].position;
    }

    public int GetWaypointCount()
    {
        return waypoints.Length;
    }
}
