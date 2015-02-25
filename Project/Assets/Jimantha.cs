using UnityEngine;
using System.Collections;

public class Jimantha : MonoBehaviour {
	
	public float speed = 0.05f;
	public int range = 20;
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
	
	
	// Use this for initialization
	void Start () {
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
		
		if((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude < _seekRange){
			_isSeeking = true;
			Debug.Log("SEEKING PLAYER");
			AssignNewWaypoint();
		} else if(_isSeeking & (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude > _seekRange) {
			_isSeeking = false;
			AssignNewWaypoint();
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
		
		if((transform.position - _currentWayPoint).magnitude < 1){
			AssignNewWaypoint();
		}
		if(_actionLengthTick > maxActionLength){
			AssignNewWaypoint();
		}
	}
	
	void AssignNewWaypoint(){
		// Set prev waypoint
		_prevWayPoint = _currentWayPoint;
		
		// Does nothing except pick a new destination
		if(!_isSeeking){
			_currentWayPoint = new Vector3(Random.Range(transform.position.x - range, transform.position.x + range), 1, Random.Range(transform.position.z - range, transform.position.z + range));
			
			Vector3 temp = _currentWayPoint - transform.position;
			temp.y = 0;
			_lookAtTarget = Quaternion.LookRotation(temp);
			
		} else if(_isSeeking){
			_currentWayPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
			
			Vector3 temp = _currentWayPoint - transform.position;
			temp.y = 0;
			_lookAtTarget = Quaternion.LookRotation(temp);
		}
		// Dont need to change direction every frame as we walk in a straight line
		// Reset lookAt animation variables
		
		_lookAtStart = transform.rotation;
		_time = 0.0f;
		_actionLengthTick = 0.0f;
		
		Debug.Log("Jimantha: " + _currentWayPoint + " and " + (transform.position - _currentWayPoint));
	}
}
