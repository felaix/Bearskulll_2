using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float _waypointMarkerSize = 0.2f;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = getNextIndex(i);
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.GetChild(i).position, _waypointMarkerSize);

            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }

    public int getNextIndex(int i)
    {
        if (i + 1 >= transform.childCount)
            return 0;
        else
            return i + 1;
    }



    public Vector3 GetWaypoint(int i)
    {

        return transform.GetChild(i).position;

    }

}

