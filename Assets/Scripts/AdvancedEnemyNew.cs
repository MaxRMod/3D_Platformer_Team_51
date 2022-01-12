using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //11Jan2022 NavMesh, NavMeshAgent, NavMeshOffMeshLink
    //AdvancedEnemy machen(Prefab) TODO


    [SerializeField] private float ChaseSpeed;  
    [SerializeField] private float NormalSpeed;
    [SerializeField] private GameObject Prey;
    private Rigidbody enemyRigidbody;

    [SerializeField] 
    private List<Transform> wayPoints;
    private int currentWayPoint = 0;
    [SerializeField] 
    private float distanceThreshold;

    private float ChaseEvadeDistance;
    private NavMeshAgent agent;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes

    public void Awake(){

<<<<<<< Updated upstream
        enemyRigidbody=GetComponent<Rigidbody>();
        agent=GetComponent<NavMeshAgent>();
//fehlt nochwas TODO
=======
    private void Awake() {
        enemyRigidbody = GetComponent<Rigidbody>();
        agent=GetComponent<NavMeshAgent>();
        agent.autoBraking=false;
        agent.destination=wayPoints[0].transform.position;
>>>>>>> Stashed changes
    }


    public enum Behaviour {
        LineOfSight,
        Intercept,
        PatternMovement,
        ChasePatternMovement,
        Hide,

        PatternMovementNavMesh
<<<<<<< Updated upstream


=======
>>>>>>> Stashed changes
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
                break;
            case Behaviour.Hide:
                if (PreyVisible(Prey.transform.position)) {
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                } else {
                    PatternMovement();
                }
                break;
            case Behaviour.PatternMovementNavMesh:
<<<<<<< Updated upstream
                if(!agent.pathPending&&agent)
                NavigateToNextPoint();
            break;
=======
                if(!agent.pathPending&&agent.remainingDistance<distanceThreshold){
                        NavigateToNextPoint();
                }   
                break; 
>>>>>>> Stashed changes
            default:
                break;
        }
    }


        private void NavigateToNextPoint(){

            currentWayPoint=(currentWayPoint+1)&wayPoints.Count;
        //    agent.destination=wayPoints;//TODO fehlt nochwas;
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

private void NavigateToNextPoint(){


    currentWayPoint=(currentWayPoint+1)%wayPoints.Count;
    agent.destination=wayPoints[currentWayPoint].transform.position;

}

}
