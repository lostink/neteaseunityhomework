using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCharacterController : MonoBehaviour {

	// Use this for initialization
	public int score;
	public GameObject networkController;
	void Start () {		
		networkController = GameObject.Find ("Network");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		var other = collision.collider.gameObject;
		Vector3 hit_position = other.transform.position;
		if (other.CompareTag ("Ball")) {
			networkController.GetComponent<PCNetwork> ().addScore (score,other);
			Destroy(gameObject);
		} else if (other.CompareTag("_myBoundary")) 
		{
			Animator thisAnimator = GetComponent<Animator> ();
			thisAnimator.StopPlayback ();
			Destroy (gameObject);
		}
	}
}
