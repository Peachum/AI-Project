using UnityEngine;
using System;
using System.Collections.Generic;

public class WaypointSystem : MonoBehaviour {

	public Color lineColour;
	public bool displayGizmos = true;
	private GameObject[] _waypoints;
	private GameObject[] testPath;

	private float waitTime = 0.0F;
	private float waitMax = 2.0F;
	private bool firstRun = true;
	private bool currentDisplayGizmos = true;

	// Use this for initialization
	void Start () {
		EnumWaypoints();
	}
	
	// Update is called once per frame
	void Update () {
		waitTime += Time.deltaTime;
		
		checkReachable();
		
		if(firstRun && waitTime >= waitMax){
			checkReachable();
			firstRun = false;
		} else if(waitTime >= waitMax){
			waitTime = 0.0F;
			
			//testPath = GenPath (_waypoints[0], getRandomWaypoint());
		}

		if(displayGizmos != currentDisplayGizmos){
			foreach(GameObject w in _waypoints){
				w.GetComponent<WaypointNode>().displayGizmos = displayGizmos;
			}

			currentDisplayGizmos = displayGizmos;
		}
	}
	
	void OnDrawGizmos(){
		if(displayGizmos && testPath != null){
			for(int i = 0; i < testPath.Length; i++)  {
				// Draws a blue line from this transform to the target
				Gizmos.color = Color.yellow;
				Gizmos.DrawCube(testPath[i].transform.position, new Vector3(3,3,3));
				
			}
		}
	}

	public void GenPath(GameObject start){
		GameObject tar = getRandomWaypoint();
		GenPath (start, tar);
	}

	public GameObject[] GenPath(GameObject start, GameObject target){
		
		_Vertex[] _vertices = new _Vertex[_waypoints.Length];
		int[,] adjMatrix = new int[_waypoints.Length, _waypoints.Length];
		int adjVertex;
		int verticesUsed = 0;
		List<_Vertex> adjList = new List<_Vertex>();

		foreach(GameObject wp in _waypoints){
			WaypointNode WaypointNodeScript = wp.GetComponent<WaypointNode>();
			_vertices[WaypointNodeScript.ID] = new _Vertex(wp);

			foreach(GameObject wp2 in WaypointNodeScript.nearbyWaypoints){
				WaypointNode WaypointNodeScript2 = wp2.GetComponent<WaypointNode>();
				adjMatrix[WaypointNodeScript.ID, WaypointNodeScript2.ID] = 1;
			}
		}

		_Vertex workingV = _vertices[start.GetComponent<WaypointNode>().ID];

		// Mark source
		workingV.isPerm = true;
		workingV.distFromSource = 0;

		while(true){

			// Check for adjacent vertices
			while(GetAdjUnvisitedVertex(workingV.index, adjMatrix, _vertices) > -1){

				// Get adjacent vertex
				adjVertex = GetAdjUnvisitedVertex(workingV.index, adjMatrix, _vertices);
				_vertices[adjVertex].wasVisited = true;

				// Add cost from from current node to distance to adjacent node
				// Check if less than previous calculations
				if(_vertices[adjVertex].distFromSource > workingV.distFromSource + Vector3.Distance(workingV.waypoint.transform.position, _vertices[adjVertex].waypoint.transform.position)){
					_vertices[adjVertex].distFromSource = workingV.distFromSource + Vector3.Distance(workingV.waypoint.transform.position, _vertices[adjVertex].waypoint.transform.position);
					_vertices[adjVertex].closestVertexIndex = workingV.index;
				}

				if(!_vertices[adjVertex].isPerm){
					adjList.Add (_vertices[adjVertex]);
				}
			}

			_Vertex smallestCost = null;
			// Loop through all adjacent vertices and find the one with the lowest cost
			foreach(_Vertex v in adjList){
				v.wasVisited = false;
				
				if(smallestCost == null){
					smallestCost = v;
				} else if(smallestCost.distFromSource > v.distFromSource){
					smallestCost = v;
				}
			}
			
			smallestCost.isPerm = true;
			workingV = smallestCost;
			adjList.Remove(workingV);
			
			if(AllPerm(_vertices)){
				break;
			}
		}
		
		// Add the nodes to the path in reverse order
		Stack<_Vertex> path = new Stack<_Vertex>();
		
		workingV = _vertices[target.GetComponent<WaypointNode>().ID];
		path.Push(workingV);
		
		while(workingV.closestVertexIndex != -1){
			// Add the closest neighbours in reverse order
			workingV = _vertices[workingV.closestVertexIndex];
			path.Push(workingV);
		}
		
		// Pop into array and flip
		GameObject[] nodePath = new GameObject[path.Count];
		int temp = path.Count;
		for(int i = 0; i < temp; i++){
			nodePath[i] = path.Pop().waypointData;
		}
		
		Array.Reverse(nodePath);
		return nodePath;

	}
	private int GetAdjUnvisitedVertex(int vertex, int[,] mat, _Vertex[] vert){
		for(int i = 0; i < _waypoints.Length; i++){
			if(mat[vertex, i] != 0 && !vert[i].wasVisited){
				return i;
			}
		}

		return -1;
	}
	private bool AllPerm(_Vertex[] vert){
		foreach(_Vertex v in vert){
			if(!v.isPerm){
				return false;
			}
		}

		return true;
	}
	private void checkReachable(){
		_Vertex[] _vertices = new _Vertex[_waypoints.Length];
		//Debug.Log(gameObject.name + " # of waypoints " + _waypoints.Length);

		foreach(GameObject wp in _waypoints){
			WaypointNode WaypointNodeScript = wp.GetComponent<WaypointNode>();
			WaypointNodeScript.isConnected = false;
			_vertices[WaypointNodeScript.ID] = new _Vertex(wp);

		}
		
		_Vertex _currentVertex = _vertices[0];
		Queue<_Vertex> verticesLeft = new Queue<_Vertex>();

		foreach(GameObject wp in _currentVertex.waypointData.GetComponent<WaypointNode>().nearbyWaypoints){
			WaypointNode WaypointNodeScript = wp.GetComponent<WaypointNode>();
			verticesLeft.Enqueue(_vertices[WaypointNodeScript.ID]);
		}

		_currentVertex.isReachable = true;
		_currentVertex.wasVisited = true;

		while(verticesLeft.Count > 0){


			_currentVertex = verticesLeft.Dequeue();

			if(_currentVertex.wasVisited){
				continue;
			} else {
				_currentVertex.isReachable = true;
				_currentVertex.wasVisited = true;

				foreach(GameObject wp in _currentVertex.waypointData.GetComponent<WaypointNode>().nearbyWaypoints){
					WaypointNode WaypointNodeScript = wp.GetComponent<WaypointNode>();
					verticesLeft.Enqueue(_vertices[WaypointNodeScript.ID]);
				}
			}
		}

		foreach(GameObject wp in _waypoints){
			WaypointNode WaypointNodeScript = wp.GetComponent<WaypointNode>();
			WaypointNodeScript.isConnected = _vertices[WaypointNodeScript.ID].isReachable;
		}
	}

	private GameObject getRandomWaypoint(){
		int temp;
		while(true){
			temp = UnityEngine.Random.Range(0, _waypoints.Length);
			
			if(!_waypoints[temp].GetComponent<WaypointNode>().isConnected){
				continue;
			} else {
				break;
			}
		}
		return _waypoints[temp];
	}

	private void EnumWaypoints(){
		GameObject[] temp = new GameObject[transform.childCount];

		for(int i = 0; i < transform.childCount; i++){
			temp[i] = transform.GetChild(i).gameObject;
			temp[i].GetComponent<WaypointNode>().ID = i;
		}

		_waypoints = temp;
	}
}
