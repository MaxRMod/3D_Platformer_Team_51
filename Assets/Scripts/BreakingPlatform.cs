using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    private Rigidbody platformRigidBody;

    void Start()
    {
        platformRigidBody = GetComponent<Rigidbody>();
    }


   
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Invoke ("DropPlatform", 0.05f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DeathZone")) {
            Destroy(this.gameObject);
        }
    }

    private void DropPlatform() {
        platformRigidBody.isKinematic = false;
    }
}
