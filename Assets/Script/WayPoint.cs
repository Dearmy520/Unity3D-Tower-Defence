using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] public static Transform[] Waypoint;

    private void Awake()
    {
        Waypoint = new Transform[transform.childCount];
        for (int i = 0; i < Waypoint.Length; i++)
        {
            Waypoint[i] = transform.GetChild(i);
        }
    }
}
