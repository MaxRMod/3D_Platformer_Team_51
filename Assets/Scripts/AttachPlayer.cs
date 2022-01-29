using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == Player) {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == Player) {
            Player.transform.parent = null;
        }
    }
}
