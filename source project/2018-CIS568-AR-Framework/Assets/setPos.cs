﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {		
		this.enabled = true;
		//this.transform.position = new Vector3 (0,1,-10);
	}
	void OnGUI()
	{
		GUILayout.Label(" ");
		GUILayout.Label(" " + this.transform.position);
	}
}
