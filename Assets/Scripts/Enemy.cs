using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed=2;
    public bool invincible=false;
    public float bumpSpeed=10;
    private Rigidbody enemyrigidbody;

    public void Awake(){

        enemyrigidbody=GetComponent<Rigidbody>();

    }

    public void FixedUpdate(){
        enemyrigidbody.velocity=new Vector3(speed,enemyrigidbody.velocity.y,0);

    }
    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("End"))
        speed *= -1;
    }

    public void OnDeath(){

        gameObject.GetComponent<Collider>().enabled=false;
    }

}
