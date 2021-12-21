using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{



[SerializeField] private float ChaseSpeed;
[SerializeField] private float NormalSpeed;
[SerializeField] private GameObject Prey;
private Rigidbody enemyRigidbody;
[SerializeField]private List<Transform>wayPoints;
private int currentWayPoint=0;  //index
[SerializeField]private float distanceThreshold;

private void Awake() {
    enemyRigidbody = GetComponent<Rigidbody>();
}

public enum Behaviour {
    LineOfSight,
    Intercept,
    PatternMovement,
    ChasePatternMovement,
    Hide
}

public Behaviour behaviour;

private void FixedUpdate() {
    switch (behaviour) {
        case Behaviour.LineOfSight:
            ChaseLineOFSight(Prey.transform.position,ChaseSpeed);

            break;
        case Behaviour.Intercept:
        Intercept(Prey.transform.position);

            break;
        case Behaviour.PatternMovement:
            PatternMovement();
            break;
        case Behaviour.ChasePatternMovement:
            if(Vector3.Distance(transform.position,Prey.transform.position)<chaseEvadeDistance);
            ChaseLineOFSight(Prey.transform.position,ChaseSpeed);
            break;
        case Behaviour.Hide:
            ChaseSpeed(Prey.)

            break;
        default:
            break;
    }

     bool PreyVisible(Vector3 targetPosition){

        Vector3 directionToTarget=targetPosition-transform.position,
        directionToTarget.Normalize();
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position,directionToTarget,out hit);
        return hit.collider.CompareTag("Player");
    }

     void ChaseLineOFSight(Vector3 targetPosition,float speed){
        Vector3 direction =targetPosition-transform.position;
        direction.Normalize();

        enemyRigidbody.velocity=new Vector3(direction.x*speed,enemyRigidbody.velocity.y,direction.z*speed);

    }

     void Intercept(Vector3 targetPosition){
        Vector3 velocityRelative,distance,predictedInterceptionPoint;
        float timeToClose;
        velocityRelative=Prey.GetComponent<Rigidbody>().velocity-enemyRigidbody.velocity;
        distance=targetPosition-transform.position;

        timeToClose=distance.magnitude/velocityRelative.magnitude;
        predictedInterceptionPoint=targetPosition+(Prey.GetComponent<Rigidbody>().velocity*timeToClose);
        var direction=predictedInterceptionPoint-transform.position;
        direction.Normalize();
        enemyRigidbody.velocity=new Vector3(direction.x*ChaseSpeed,enemyRigidbody.velocity.y,direction.z*ChaseSpeed);





    }

    void PatternMovement(){

        ChaseLineOFSight(wayPoints[currentWayPoint].position,NormalSpeed);

        if(Vector3.Distance(gameObject.transform.position, wayPoints[currentWayPoint].position)<distanceThreshold){
                currentWayPoint=(currentWayPoint+1) % wayPoints.Count;

        }
    }


}
}
