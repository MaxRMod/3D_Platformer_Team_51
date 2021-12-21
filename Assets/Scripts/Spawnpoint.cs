using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
   
   
    void OnDrawGizmos(){
        //comment
        Gizmos.DrawIcon(gameObject.transform.position,"spawnpoint");
    }
   
   
    
}
