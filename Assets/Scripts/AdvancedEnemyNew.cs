using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemyNew : MonoBehaviour
{
    [SerializeField] private float ChaseSpeed;  
    [SerializeField] private float NormalSpeed;
    [SerializeField] private GameObject Prey;
    private Rigidbody enemyRigidbody;

    [SerializeField] 
    private List<Transform> wayPoints;
    private int currentWayPoint = 0;
    [SerializeField] 
    private float distanceThreshold;

    [SerializeField]private float ChaseEvadeDistance;

private NavMeshAgent agent;

    private void Awake() {
        enemyRigidbody = GetComponent<Rigidbody>();
        agent=GetComponent<NavMeshAgent>();
        agent.autoBraking=false;
        agent.destination=wayPoints[0].transform.position;
    }

    public enum Behaviour {
        LineOfSight,
        Intercept,
        PatternMovement,
        ChasePatternMovement,
        Hide,

        PatternMovementBehaviour
    }

    public Behaviour behaviour;

    private void FixedUpdate() {
        switch (behaviour) {
            case Behaviour.LineOfSight:
                ChaseLineOfSight(Prey.transform.position,ChaseSpeed);
                break;
            case Behaviour.Intercept:
                Intercept(Prey.transform.position);
                break;
            case Behaviour.PatternMovement:
                PatternMovement();
                break;
            case Behaviour.ChasePatternMovement:
                if (Vector3.Distance(transform.position, Prey.transform.position) < ChaseEvadeDistance){
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                }
                else{
                    PatternMovement();
                }
                break;
            case Behaviour.Hide:
                if (PreyVisible(Prey.transform.position)) {
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                } else {
                    PatternMovement();
                }
                break;
            case Behaviour.PatternMovementBehaviour:
                
                if(!agent.pathPending&&agent.remainingDistance<distanceThreshold){

                    NavigateToNextPoint();
                }
                break;    
            default:
                break;
        }
    }

    public void NavigateToNextPoint(){

        currentWayPoint=(currentWayPoint+1)%wayPoints.Count;
        agent.destination=wayPoints[currentWayPoint].transform.position;
    }

    private bool PreyVisible(Vector3 targetPosition) {
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.Normalize();

        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, directionToTarget, out hit);

        return hit.collider.CompareTag("Player");
    }


    private void ChaseLineOfSight(Vector3 targetPosition, float Speed) {
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z*Speed);
    }

    private void Intercept(Vector3 targetPosition) {
        Vector3 enemyPosition=gameObject.transform.position;
        Vector3 preyPosition=Prey.transform.position;
        float timeToClose;
        
        var VelocityRelative = Prey.GetComponent<Rigidbody>().velocity - enemyRigidbody.velocity;
        var Distance = targetPosition - transform.position;

        timeToClose = Distance.magnitude / VelocityRelative.magnitude;

        var PredictedInterceptionPoint = targetPosition + (Prey.GetComponent<Rigidbody>().velocity * timeToClose);

        var direction = PredictedInterceptionPoint - transform.position;
        direction.Normalize();

        enemyRigidbody.velocity = new Vector3(direction.x * ChaseSpeed, enemyRigidbody.velocity.y, direction.z * ChaseSpeed);

    }

    private void PatternMovement() {
        ChaseLineOfSight(wayPoints[currentWayPoint].position, NormalSpeed);

        if (Vector3.Distance(gameObject.transform.position, wayPoints[currentWayPoint].position) < distanceThreshold) {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Count;
        }
    }
}
