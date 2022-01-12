using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatformBehav : MonoBehaviour
{
    private Rigidbody playRbody;
    private Rigidbody platrody;
    private bool isOnPlat;

    private void Awake(){
        playRbody=GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision enter){
        if(enter.gameObject.tag=="MovingPlatform"){
            isOnPlat=true;
            platrody=enter.gameObject.GetComponent<Rigidbody>();
        }
    }

    void OnCollisionExit(Collision exit){
        if(exit.gameObject.tag=="MovingPlatform"){
            isOnPlat=false;
            platrody=null;
        }
    }

    void FixedUpdate(){
        if(isOnPlat){
            playRbody.velocity+=platrody.velocity;
        }

    }
}
