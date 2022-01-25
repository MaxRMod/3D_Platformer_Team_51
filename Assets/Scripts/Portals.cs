using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    [SerializeField]private GameObject portal2;
    [SerializeField]private GameObject offset;
    //[SerializeField]private Transform teleportPoint;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Portal2")) {
                other.transform.position = new Vector3(portal2.transform.position.x + 0.4f, portal2.transform.position.y + 0.45f, portal2.transform.position.z + 0.4f);
            } else if (gameObject.CompareTag("Portal1")) {
                other.transform.position = new Vector3(portal2.transform.position.x - 0.4f, portal2.transform.position.y + 0.45f, portal2.transform.position.z - 0.4f);
            }
            other.transform.rotation = portal2.transform.rotation;
        }
    }
}
