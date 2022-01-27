using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    

    private void OnDrawGizmos(){

        Gizmos.DrawSphere(transform.position,0.2f);//zuvor 5f, mal gucken ob relevant, falls was nicht funktioniert, zurück zu 5
    }
}
