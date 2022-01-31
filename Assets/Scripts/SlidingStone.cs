using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingStone : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody>().velocity == Vector3.zero) {
            this.transform.position = spawnPoint.transform.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.one;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.one;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DeathZone")) {
            this.transform.position = spawnPoint.transform.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.one;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.one;
        }
    }
}
