using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    //Variables of both Settings classes
    public MoveSettings moveSettings;
    public InputSettings inputSettings;

    [SerializeField] private Transform spawnPoint;
    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, jumpInput;
    private Vector3 initialScale;

    //Jump-variables
    private int JumpCount = 0;
    private float fallDownFaster=1.7f;
    [SerializeField] private int MaxJumps = 2; //Maximum amount of jumps

    Rigidbody platformRigidbody;
    bool isOnPlatform = false;

    //SpeedBoost variables
    private float speedBoostTimer;
    private bool speedBoosted;

    //JumpBoost variables
    private float jumpBoostTimer;
    private bool jumpBoosted;

    //Collectible variables
    private int collectibleCounter;
    public Text collectibleText;

    //KeyItem variables
    private bool hasKey;

    //Set all starting values
    void Awake()
    {
        Spawn();
        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speedBoosted = false;
        speedBoostTimer = 0;
        jumpBoosted = false;
        speedBoostTimer = 0;
        JumpCount = MaxJumps;
        collectibleCounter = 0;
    }

    //Called every frame
    void Update()
    {
        if(playerRigidbody.velocity.y<0){
            playerRigidbody.velocity+=Vector3.up*Physics.gravity.y*fallDownFaster*Time.deltaTime;
        }
        Turn();
        GetInput();

        if (speedBoosted)
        {
        
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer >= 3)
            {
                moveSettings.runVelocity /= 2;
                speedBoostTimer = 0;
                speedBoosted = false;
            }
            
        }

        if (jumpBoosted)
        {
        
            jumpBoostTimer += Time.deltaTime;
            if (jumpBoostTimer >= 3)
            {
                moveSettings.jumpVelocity /= 2;
                jumpBoostTimer = 0;
                jumpBoosted = false;
            }
            
        }
    }

    //Called every timestep
    void FixedUpdate()
    {
        Run();
        if (Input.GetButton("Jump"))
        {
            if (JumpCount > 0)
            {
                Jump();
            }
        }
    }

    #region PlayerMovement
    //Saves user input
    void GetInput()
    {
        if (inputSettings.FORWARD_AXIS.Length != 0)
        {
            forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        }
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
        { 
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        }
        if (inputSettings.TURN_AXIS.Length != 0)
        {
            turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
        }
        if (inputSettings.JUMP_AXIS.Length != 0)
        { 
            jumpInput = Input.GetAxis(inputSettings.JUMP_AXIS);
        }
    }

    void Run()
    {
       if(!IsGrounded()){
            
            velocity.x=playerRigidbody.velocity.x;
            velocity.y=playerRigidbody.velocity.y;
            velocity.z=playerRigidbody.velocity.z;
        //if(isOnPlatform){
        //  playerRigidbody.velocity+=platformRigidbody.velocity;
        // }     
           playerRigidbody.velocity = transform.TransformDirection(velocity);
        }
        else{
            velocity.y = playerRigidbody.velocity.y;
            velocity.z = forwardInput * moveSettings.runVelocity;
            velocity.x = sidewaysInput * moveSettings.runVelocity;
            playerRigidbody.velocity = transform.TransformDirection(velocity);
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }


    void Jump()
    {
        if (jumpInput != 0 && IsGrounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, moveSettings.jumpVelocity, playerRigidbody.velocity.z);
            JumpCount -= 1;
        }


    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.distanceToGround, moveSettings.ground);
    }
    #endregion

    void JumpedOnEnemy(float bumpSpeed){

        playerRigidbody.velocity=new Vector3(playerRigidbody.velocity.x,bumpSpeed,playerRigidbody.velocity.z);
    }

    void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Enemy")){
                Enemy enemy=collision.gameObject.GetComponent<Enemy>();
                Collider col=collision.gameObject.GetComponent<Collider>();
                Collider mycol=this.gameObject.GetComponent<Collider>();

               
               //TODO
               //Nochmal PDF angucken und Code überarbeiten, sonst gibt es hier ein Problem  mit bumpSpeed
               /* if(enemy.invincible)
                {
                    OnDeath();
                }
                else if(mycol.bounds.center.y-mycol.bounds.extents.y>col.bounds.center.y+0.5f*col.bounds.extents.y)
                {
                    GameData.Instance.Score+=10;
                    JumpedOnEnemy(enemy.bumpSpeed);
                    enemy.OnDeath();
                }
                else
                {
                    OnDeath();
                }
                */
        }

        if(collision.gameObject.CompareTag("MovingPlatform")){
            //transform.parent
            platformRigidbody=collision.gameObject.GetComponent<Rigidbody>();
            isOnPlatform=true;
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            JumpCount = MaxJumps;
        }
    }

    void Spawn(){

        transform.position=spawnPoint.position;
    }

    public void OnDeath(){

        Spawn();
    }

    void StopMovingWithPlatform(Collision collisionExit){

        
        if(collisionExit.gameObject.tag=="MovingPlatform"){
            platformRigidbody=null;
            isOnPlatform=false;
        }
    }

    


    

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("DeathZone"))
        {
            Spawn();
        }

        if (other.CompareTag("Checkpoint"))
        {
            spawnPoint = other.gameObject.transform;
        }

        if (other.CompareTag("SpeedBoost"))
        {
            speedBoosted = true;
            moveSettings.runVelocity *= 2;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("JumpBoost"))
        {
            jumpBoosted = true;
            moveSettings.jumpVelocity *= 2;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Collectible"))
        {
            collectibleCounter++;
            collectibleText.text = "Coins: " + collectibleCounter;
            Destroy(other.gameObject);
        }
        
    
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }

    }
}

[System.Serializable]
public class MoveSettings{

    public float runVelocity = 12;
    public float rotateVelocity = 100;
    public float jumpVelocity = 8;
    public float distanceToGround = 1.3f;
    public LayerMask ground; 

}
[System.Serializable]
public class InputSettings
{
    public string FORWARD_AXIS = "Vertical";
    public string SIDEWAYS_AXIS = "Horizontal";
    public string TURN_AXIS = "Mouse X";
    public string JUMP_AXIS = "Jump";
}
