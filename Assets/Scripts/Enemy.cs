using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float speed=2;
    public bool invincible=false;
    public float bumpSpeed=10;
    private Rigidbody enemyRigidbody;

    public void Awake(){

        enemyRigidbody=GetComponent<Rigidbody>();

    }

    public void FixedUpdate(){
        enemyRigidbody.velocity=new Vector3(speed,enemyRigidbody.velocity.y,0);

    }
    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("End"))
        speed *= -1;
    }

    public void OnDeath(){

        gameObject.GetComponent<Collider>().enabled=false;
    }

}
