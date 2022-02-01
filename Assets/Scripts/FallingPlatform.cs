using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody platformRigidBody;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float waitTime = 2f;
    private bool triggered;

    private float fallingTimer;
    

    void Start()
    {
        fallingTimer = 0;
        triggered = false;
        platformRigidBody = GetComponent<Rigidbody>();
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
    }

    private void Update() {
        if (triggered) {
            fallingTimer += Time.deltaTime;
            if (fallingTimer >= 2)
            {
                platformRigidBody.isKinematic = true;
                this.transform.position = initialPosition;
                this.transform.rotation = initialRotation;
                fallingTimer = 0;
                triggered = false;
            }
        }
    }
   
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Invoke ("DropPlatform", 0.2f);
            triggered = true;
        }
    }

    private void DropPlatform() {
        platformRigidBody.isKinematic = false;
    }
}
