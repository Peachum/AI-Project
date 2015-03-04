﻿using UnityEngine;
using System.Collections;

public class Jimario : MonoBehaviour {
	
	public float speed = 0.05f;
	public int range = 10;
	public float turnspeed = 1.0f;
	public float maxActionLength = 10.0f;
	
	private float _currentSpeed;
	private Vector3 _prevWayPoint;
	private Vector3 _currentWayPoint;
	private float _minSpeed = 0.00f;
	private float _acceleration = 0.05f;
	private int _seekRange = 20;
	
	private Quaternion _lookAtStart;
	private Quaternion _lookAtTarget;
	private float _time = 0.0f;
	
	private float _actionLengthTick = 0.0f;
	
	private bool _isSeeking = false;

	private GameObject[] _waypoints;
	private int _currentWaypointNumber = 0;
	
	
	// Use this for initialization
	void Start () {
		_waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		_currentSpeed = speed;
		_currentWayPoint = transform.position;
		AssignNewWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 temp = transform.position;
		temp += transform.TransformDirection(Vector3.forward)*_currentSpeed;
		transform.position = temp;
		
		_time += Time.deltaTime;
		_actionLengthTick += Time.deltaTime;
		
		if(_time < turnspeed){
			_lookAtStart = transform.rotation;
			transform.rotation = Quaternion.Slerp(_lookAtStart, _lookAtTarget, _time/turnspeed);
		}

		if((transform.position - _currentWayPoint).magnitude < 3 && (transform.position - _currentWayPoint).magnitude > 1){
			if(_currentSpeed > _minSpeed){
				_currentSpeed -= _acceleration * Time.deltaTime;
			}
			if((transform.position - _currentWayPoint).magnitude < 2)
				AssignNewWaypoint();
		}

		if(!((transform.position - _prevWayPoint).magnitude < 3)){
			if(_currentSpeed < speed){
				_currentSpeed += _acceleration * Time.deltaTime;
			}
		}

		if((transform.position - _currentWayPoint).magnitude < 1){
			AssignNewWaypoint();
		}
		if(_actionLengthTick > maxActionLength){
			//Debug.Log ("Jimario Timeout");
			AssignNewWaypoint();
		}
		
		/*if((transform.position - GameObject.FindGameObjectWithTag("Waypoint").transform.position).magnitude < _seekRange){
			_isSeeking = true;
			//Debug.Log("SEEKING Jimmy");
			AssignNewWaypoint();
		} else if(_isSeeking & (transform.position - GameObject.FindGameObjectWithTag("Waypoint").transform.position).magnitude > _seekRange) {
			_isSeeking = false;
			Debug.Log("Stopped seeking player");
		}else if(_isSeeking){
			AssignNewWaypoint();
		}
		
		//if(!_isSeeking){
		if((transform.position - _currentWayPoint).magnitude < 3 && (transform.position - _currentWayPoint).magnitude > 1){
			// When distance between us and target is less than 3, slow down
			if(_currentSpeed > _minSpeed){
				_currentSpeed -= _acceleration * Time.deltaTime;
			}
		} 
		if(!((transform.position - _prevWayPoint).magnitude < 3)){
			// When distance between us and previous target is greater than 3, speed up
			if(_currentSpeed < speed){
				_currentSpeed += _acceleration * Time.deltaTime;
			}
		}
		//}
		*/
	}
	
	void AssignNewWaypoint(){
		// Set prev waypoint
		_prevWayPoint = _currentWayPoint;
		_currentWaypointNumber++;
		//Debug.Log ("current waypoint number: " + _currentWaypointNumber);

		if (_currentWaypointNumber >= _waypoints.Length) {
			_currentWaypointNumber = 0;
		}


		_currentWayPoint = _waypoints[_currentWaypointNumber].transform.position;

		Vector3 temp = _currentWayPoint - transform.position;
		temp.y = 0;
		_lookAtTarget = Quaternion.LookRotation(temp);

		/*
		// Does nothing except pick a new destination
		if(!_isSeeking){
			_currentWayPoint = new Vector3(Random.Range(transform.position.x - range, transform.position.x + range), 1, Random.Range(transform.position.z - range, transform.position.z + range));
			
			Vector3 temp = _currentWayPoint - transform.position;
			temp.y = 0;
			_lookAtTarget = Quaternion.LookRotation(temp);
			
		} else if(_isSeeking){
			_currentWayPoint = GameObject.FindGameObjectWithTag("Waypoint").transform.position;
			
			Vector3 temp = _currentWayPoint - transform.position;
			temp.y = 0;
			_lookAtTarget = Quaternion.LookRotation(temp);
		}
		*/


		// Dont need to change direction every frame as we walk in a straight line
		// Reset lookAt animation variables
		
		_lookAtStart = transform.rotation;
		_time = 0.0f;
		_actionLengthTick = 0.0f;
		
		//Debug.Log("Jimario: " + _currentWayPoint + " and " + (transform.position - _currentWayPoint));
	}
}
