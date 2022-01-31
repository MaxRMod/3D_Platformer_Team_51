using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed;
	
	void Start()
	{
		
	}

	void Update()
	{
    	transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
	}
}
