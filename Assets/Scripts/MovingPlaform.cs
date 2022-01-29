using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlaform : MonoBehaviour

{
[SerializeField] private Transform PointA;
[SerializeField] private Transform PointB;
[SerializeField] private float speed=2f;

private Rigidbody rbody;
 private Transform currentStart;
 private Transform currentTarget;

private float distance=0f;

private float playerVelocity = 0f;


private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rbody=GetComponent<Rigidbody>();
        currentStart=PointA;
        currentTarget=PointB;
    
    }

    // Update is called once per frame
    void Update()
    {
        distance=distance+speed*Time.deltaTime;
        transform.position=Vector3.Lerp(currentStart.position,currentTarget.position,distance);
        rbody.MovePosition(Vector3.Lerp(currentStart.position,currentTarget.position,distance));
        if(distance>1){
            distance=0;
            Transform oldTarget=currentTarget;
            currentTarget=currentStart;
            currentStart=oldTarget;
        }
    }

    
    void OnTriggerEnter(Collider other){
        other.gameObject.transform.SetParent(gameObject.transform);
        playerVelocity = other.gameObject.GetComponent<PlayerBehaviour>().moveSettings.runVelocity;
        other.gameObject.GetComponent<PlayerBehaviour>().moveSettings.runVelocity = 10;
    }

    void OnCollisionExit(Collision exit){
        exit.gameObject.transform.SetParent(null);
        exit.gameObject.GetComponent<PlayerBehaviour>().moveSettings.runVelocity = playerVelocity;
    }


}

