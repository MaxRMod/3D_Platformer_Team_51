using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : Enemy
{
    public float speed;

     public void FixedUpdate(){
        enemyRigidbody.velocity=new Vector3(speed,enemyRigidbody.velocity.y,0);

    }

    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("End"))
        speed *= -1;
    }
}
