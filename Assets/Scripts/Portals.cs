using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    [SerializeField]private GameObject linkedPortal;
    [SerializeField]public GameObject teleportPoint;
    [SerializeField]private Quaternion rotation;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per framelinkedPortal
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player"))
        {
            var yRotation = linkedPortal.transform.eulerAngles.y;
            if (gameObject.CompareTag("Portal2")) {
                //other.transform.position = new Vector3(linkedPortal.transform.position.x + 0.4f, linkedPortal.transform.position.y + 0.45f, linkedPortal.transform.position.z + 0.4f);
                GameObject point = linkedPortal.GetComponent<Portals>().teleportPoint;
                other.transform.position = new Vector3(point.transform.position.x, point.transform.position.y, point.transform.position.z);
                other.transform.Rotate(other.transform.eulerAngles.x, yRotation, other.transform.eulerAngles.z);
            } else if (gameObject.CompareTag("Portal1")) {
                GameObject point = linkedPortal.GetComponent<Portals>().teleportPoint;
                other.transform.position = new Vector3(point.transform.position.x, point.transform.position.y, point.transform.position.z);
                other.transform.Rotate(other.transform.eulerAngles.x, yRotation, other.transform.eulerAngles.z);
            }
        }
    }
}
