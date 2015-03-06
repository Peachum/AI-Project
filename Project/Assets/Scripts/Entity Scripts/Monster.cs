using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	
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
	private enum MonsterStates {
		IDLE = 0,
		WANDER,
		PATHING,
		FLEE,
		SEEK,
	}

	private MonsterStates _currentState;
	private MonsterStates _prevState;

	//------------------------------
	// Use this for initialization
	//------------------------------
	void Start () {
		_currentState = MonsterStates.IDLE;
	}

	//------------------------------
	// Update is called once per frame
	//------------------------------
	void Update () {
		switch (_currentState) {
			case MonsterStates.IDLE: break;
			case MonsterStates.WANDER: break;
			case MonsterStates.PATHING: break;
			case MonsterStates.FLEE: break;
			case MonsterStates.SEEK: break;
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
