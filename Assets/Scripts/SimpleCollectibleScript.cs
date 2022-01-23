using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for pickup rotation. must be assigned to pickups
public class SimpleCollectibleScript : MonoBehaviour {
	[SerializeField]
	public float rotationSpeed;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}


}
