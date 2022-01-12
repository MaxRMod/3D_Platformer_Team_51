using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlaform : MonoBehaviour

{
[SerializeField] private Transform PointA;
[SerializeField] private Transform PointB;
[SerializeField] private float speed=2f;

 private Transform currentStart;
 private Transform currentTarget;

private float distance=0f;

[SerializeField] bool smthIsOnPlatform=false;
[SerializeField] GameObject currentlyOnPlatform=null;
private Vector3 offset;









    // Start is called before the first frame update
    void Start()
    {
        currentStart=PointA;
        currentTarget=PointB;
    }

    // Update is called once per frame
    void Update()
    {

            distance=distance+speed*Time.deltaTime;
            transform.position=Vector3.Lerp(currentStart.position,currentTarget.position,distance);
            if(distance>1){

                distance=0;
                Transform oldTarget=currentTarget;
                currentTarget=currentStart;
                currentStart=oldTarget;
            }




    }
/*void LateUpdate(){
        if(smthIsOnPlatform){
            //currentlyOnPlatform.transform.position=transform.position+offset;
        }

    }
*/

    void OnCollisionEnter(Collision col){
        col.gameObject.transform.SetParent(gameObject.transform,true);
        currentlyOnPlatform=col.gameObject;
        smthIsOnPlatform=true;
        //offset=col.transform.position-transform.position;
    }

    void OnCollisionExit(Collision exit){

        exit.gameObject.transform.parent=null;
        smthIsOnPlatform=false;
        currentlyOnPlatform=null;
    }
/*    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Player"){
                other.transform.parent=transform;
                currentlyOnPlatform=other.gameObject;
                smthIsOnPlatform=true;
                offset=other.transform.position-transform.position;
        }
    }

    void OnTriggerExit(Collider colliderExit){
            
        if(colliderExit.gameObject.tag=="Player"){
                colliderExit.transform.parent=null;
        }
            currentlyOnPlatform=null;
            smthIsOnPlatform=false;
        
    }
*/

}
