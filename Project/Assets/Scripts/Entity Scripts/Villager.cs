using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

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

	private int _actionTimer = 0;

	//------------------------------
	// States
	//------------------------------
	private enum VillagerStates {
		IDLE = 0,
		WANDER,
		PATHING,
		FLEE,
		SEEK,
	}

	private VillagerStates _currentState;
	private VillagerStates _prevState;

	//------------------------------
	// Use this for initialization
	//------------------------------
	void Start () {
		_currentState = VillagerStates.IDLE;
	}

	//------------------------------
	// Update is called once per frame
	//------------------------------
	void Update () {
		switch (_currentState) {
		case VillagerStates.IDLE: break;
		case VillagerStates.WANDER: break;
		case VillagerStates.PATHING: break;
		case VillagerStates.FLEE: break;
		case VillagerStates.SEEK: break;
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
