using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    
    public bool invincible=false;
    public float bumpSpeed=10;
    protected Rigidbody enemyRigidbody;

    public void Awake(){

        enemyRigidbody=GetComponent<Rigidbody>();

    }



    public void OnDeath(){

        gameObject.GetComponent<Collider>().enabled=false;
        gameObject.SetActive(false);
    }

}
