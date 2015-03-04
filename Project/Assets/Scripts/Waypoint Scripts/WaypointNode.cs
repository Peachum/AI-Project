using UnityEngine;
using System.Collections;

public class WaypointNode : MonoBehaviour {

	public float connectRange = 20.0F;
	public LayerMask waypointLayer;
	private RaycastHit _hit;
	private const float _waypointCheckCooldown = 5.0F;
	private float _waypointCheckTick = 0.0F;
	private Color _lineColour;

	[System.NonSerialized]
	public int ID = int.MaxValue;
	[System.NonSerialized]
	public GameObject[] nearbyWaypoints;
	[System.NonSerialized]
	public bool isConnected = false;
	[System.NonSerialized]
	public bool displayGizmos = true;

	// Use this for initialization
	void Start () {
		GetNearbyWaypoints();
		if(gameObject.transform.parent != null)
			_lineColour = gameObject.transform.parent.gameObject.GetComponent<WaypointSystem>().lineColour;
	}
	
	// Update is called once per frame
	void Update () {
		_waypointCheckTick += Time.deltaTime;

		if(_waypointCheckTick >= _waypointCheckCooldown){
			GetNearbyWaypoints();
			_waypointCheckTick = 0.0F;
		}
	}

	// Draws lines to connected waypoints
	void OnDrawGizmos(){
		if(displayGizmos && nearbyWaypoints != null){
			for(int i = 0; i < nearbyWaypoints.Length; i++)  {
				// Draws a blue line from this transform to the target
				GameObject target = nearbyWaypoints[i];
				if(isConnected){
					Gizmos.color = _lineColour;
				} else {
					Gizmos.color = Color.red;
				}
				Gizmos.DrawLine (gameObject.transform.position, target.transform.position);
			}
		}
	}

	// Draws range sphere for waypoint connecting
	void OnDrawGizmosSelected(){
		if(displayGizmos){
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(gameObject.transform.position, connectRange);
		}
		//Debug.Log(ID);
	}

	void GetNearbyWaypoints(){
		if(gameObject.transform.parent == null){
			return;
		}
		Collider[] cols;

		cols = Physics.OverlapSphere(gameObject.transform.position, connectRange, waypointLayer);

		int tempLength = 0;

		// Find which nearby points are visible
		for(int i = 0; i < cols.Length; i++){

			if(gameObject.transform.parent != cols[i].gameObject.transform.parent){
				continue;
			} else {
				Vector3 dir = cols[i].gameObject.transform.position - gameObject.transform.position;

				if(Physics.Raycast( gameObject.transform.position, dir, out _hit)){
					if(_hit.transform == cols[i].gameObject.transform){
						tempLength++;
						//Debug.Log("hit" + cols[i].gameObject);
					}
				}
			}
		}

		nearbyWaypoints = new GameObject[tempLength];

		// Add them to the new array
		int iterator = 0;
		for(int i = 0; i < cols.Length; i++){

			if(gameObject.transform.parent != cols[i].gameObject.transform.parent){
				continue;
			} else {
				Vector3 dir = cols[i].gameObject.transform.position - gameObject.transform.position;
				
				if(Physics.Raycast( gameObject.transform.position, dir, out _hit)){
					if(_hit.transform == cols[i].gameObject.transform){
						nearbyWaypoints[iterator] = cols[i].gameObject;
						iterator++;
					}
				}
			}
		}
		
		//Debug.Log(gameObject.transform.parent.name + "| Waypoints found" + nearbyWaypoints.Length);
			
	}
}
