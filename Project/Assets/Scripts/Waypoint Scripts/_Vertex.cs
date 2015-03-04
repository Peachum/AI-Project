using UnityEngine;
using System.Collections;

public class _Vertex
{
	public GameObject waypointData;
	public WaypointNode waypoint;
	public int closestVertexIndex;
	public bool wasVisited;
	public bool isPerm;
	public bool isReachable;
	public float distFromSource;
	public int index;

	public _Vertex (GameObject w)
	{
		waypoint = w.GetComponent<WaypointNode>();
		waypointData = w;
		wasVisited = false;
		isPerm = false;
		distFromSource = float.MaxValue;
		isReachable = waypoint.isConnected;
		index = waypoint.ID;
		closestVertexIndex = - 1;
	}
}


