using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearingPlatform : MonoBehaviour
{
    IEnumerator OnCollisionEnter (Collision coll) {
 
    yield return new WaitForSeconds(2);//wait x amount of seconds
    Destroy(gameObject);
 
 }
}
