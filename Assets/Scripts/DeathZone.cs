using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDrawGizmos(){

        Gizmos.DrawIcon(gameObject.transform.position,"DeathZone");
    }

}
