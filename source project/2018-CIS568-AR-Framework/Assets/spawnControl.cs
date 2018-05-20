using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnControl : MonoBehaviour {

	// Use this for initialization
	public GameObject Target;
	public Vector3 targetRotation;
	public float spawnRate;
	public float delay;
	public int ScoreFromThisPoint;
	private float nextTime;
	void Start () {
		nextTime = Time.time + spawnRate + delay;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextTime) {
			GameObject target = Instantiate(Target, transform.position, Quaternion.Euler(targetRotation)) as GameObject;
			target.GetComponent<targetCharacterController> ().score = ScoreFromThisPoint;
			nextTime = Time.time + spawnRate + Random.value;
		}
	}
}
