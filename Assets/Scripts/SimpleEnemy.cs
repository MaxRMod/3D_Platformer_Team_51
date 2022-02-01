using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : Enemy
{

    [SerializeField] bool zAxisMove=false;
    public float speed=2;

     public void FixedUpdate(){
        if(!zAxisMove){
            enemyRigidbody.velocity=new Vector3(speed,enemyRigidbody.velocity.y,0);
        }
        else{
            enemyRigidbody.velocity=new Vector3(0,enemyRigidbody.velocity.y,speed);
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("End"))
        speed *= -1;
    }

}
