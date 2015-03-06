using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

	
	//------------------------------
	// Set in Inspector
	//------------------------------
	public int speed;
	public int turnSpeed;
	public int wanderRadius;

	//------------------------------
	// Non-serialized attributes
	//------------------------------
	[System.NonSerialized]
	public bool displayGizmos = false;

	//------------------------------
	// Private attributes
	//------------------------------
	private float _currentSpeed;
	private Vector3 _currentTarget;
	private Vector3 _prevTarget;
	
	private Quaternion _lookAtStart;
	private Quaternion _lookatTarget;

	//------------------------------
	// States
	//------------------------------
	private enum AnimalStates {
		IDLE = 0,
		WANDER,
		PATHING,
		FLEE,
		SEEK,
	}

	private AnimalStates _currentState;
	private AnimalStates _prevState;

	//------------------------------
	// Use this for initialization
	//------------------------------
	void Start () {
		_currentState = AnimalStates.IDLE;
	}

	//------------------------------
	// Update is called once per frame
	//------------------------------
	void Update () {
		switch (_currentState) {
		case AnimalStates.IDLE: break;
		case AnimalStates.WANDER: break;
		case AnimalStates.PATHING: break;
		case AnimalStates.FLEE: break;
		case AnimalStates.SEEK: break;
		default: break;
		}
	}

	//------------------------------
	// TODO
	//------------------------------
	void Idle(){
		
	}
	
	//------------------------------
	// TODO
	//------------------------------
	void Wander(){
		
	}
	
	//------------------------------
	// Move to the currently set target
	//------------------------------
	void Move(){
		
	}
	
	//------------------------------
	// Flees directly opposite of
	// entity it is fleeing from
	//------------------------------
	void Flee(){
		
	}
	
	//------------------------------
	// Gets next target from pathfinding list
	// or finds a random target to wander to
	//------------------------------
	void FindTarget(){
		
	}
}
