using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformJumping : MonoBehaviour
{

    [SerializeField] private Rigidbody rbody;
    [SerializeField] private Rigidbody platformRbody;
    private bool isOnPlatform;

    private void Awake(){

        rbody=GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision enter){}
    void OnCollisionExit(Collision exit){}
}
