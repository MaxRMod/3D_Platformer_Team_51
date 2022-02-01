using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody platformRigidBody;
    void Start()
    {
        platformRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Invoke ("DropPlatform", 0.5f);
            Destroy (gameObject, 2f);
        }
    }

    private void DropPlatform() {
        platformRigidBody.isKinematic = false;
    }
}
