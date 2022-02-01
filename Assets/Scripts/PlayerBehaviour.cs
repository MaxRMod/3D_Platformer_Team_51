using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    //Variables of both Settings classes
    public MoveSettings moveSettings;
    public InputSettings inputSettings;

    [SerializeField] private Transform spawnPoint;
    private Rigidbody playerRigidbody;
    public Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, jumpInput;
    private Vector3 initialScale;
    public Animator anim;

    public Transform cam;
    //Jump-variables
    private int JumpCount = 0;
    private float fallDownFaster=1f;
    [SerializeField] private int MaxJumps = 2; //Maximum amount of jumps

    Rigidbody platformRigidbody;

    public GameObject pickupEffect;
    public GameObject explosion;

    //SpeedBoost variables

    List<GameObject> boosterList = new List<GameObject>();

    [SerializeField]private float speedBoostTimer;
    private bool speedBoosted;
    private float initialRunSpeed;

    //JumpBoost variables
    [SerializeField]private float jumpBoostTimer;
    private bool jumpBoosted;
    private float initialJumpSpeed;

    //Collectible variables
    private int collectibleCounter;
    public Text collectibleText;

    public Text achievementText;

    //KeyItem variables
    private bool hasKey;

    //Portal variables
    private Transform portalSpawnPoint;

    //Health variables
    private int lives;
    public HealthManager currentManager;

    //Sound variables
    public AudioSource coinPickup;
    public AudioSource speedPickup;
    public AudioSource jumpPickup;
    public AudioSource keyPickup;
    public AudioSource healPickup;

    //Run Sounds
    private float runSoundTimer;
    public AudioClip[] clips;
    public AudioSource audioSource;
    private bool stepped;

    

    //Set all starting values
    void Awake()
    {
        Spawn();
        Cursor.visible = false;
        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        initialRunSpeed = moveSettings.runVelocity;
        initialJumpSpeed = moveSettings.jumpVelocity;
    }

    private void Start()
    {
        speedBoosted = false;
        speedBoostTimer = 0;
        jumpBoosted = false;
        speedBoostTimer = 0;
        JumpCount = 0;
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            GameData.Instance.Coins = 0;
        }
        collectibleCounter = GameData.Instance.Coins;
        //collectibleText = GameObject.Find("collectibleText").GetComponent<Text>();
        stepped = false;
        runSoundTimer = 0;

        //collectibleText=GameObject.Find("collectibleText").GetComponent<Text>();
        updateStats();

        
    }
    public void updateStats(){
        this.collectibleText.text="Coins: " + collectibleCounter;

    }

    //Called every frame
    void Update()
    {

        if(playerRigidbody.velocity.y<0){
            playerRigidbody.velocity+=Vector3.up*Physics.gravity.y*fallDownFaster*Time.deltaTime;
        }
        Turn();
        GetInput();

        anim.SetBool("isGrounded", !Input.GetButton("Jump"));
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        anim.SetBool("RunningRight", turnInput > 0.2);
        anim.SetBool("RunningLeft", turnInput < -0.2);

        if (forwardInput != 0 && IsGrounded()|| sidewaysInput != 0 && IsGrounded()) {
            if (!stepped) {
                stepped = true;
                audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
            }
        }

        if (stepped)
        {
        
            runSoundTimer += Time.deltaTime;
            if (runSoundTimer >= 0.5)
            {
                runSoundTimer = 0;
                stepped = false;
            }
            
        }

        if (speedBoosted)
        {
        
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer >= 6)
            {
                moveSettings.runVelocity  = initialRunSpeed;
                speedBoostTimer = 0;
                speedBoosted = false;
            }
            
        }

        if (jumpBoosted)
        {
        
            jumpBoostTimer += Time.deltaTime;
            if (jumpBoostTimer >= 6)
            {
                moveSettings.jumpVelocity = initialJumpSpeed;
                jumpBoostTimer = 0;
                jumpBoosted = false;
            }
            
        }

        lives = currentManager.currentLives;

        if (lives <= 0) {
            currentManager.ResetLives();
            Spawn();
        }

        if (collectibleCounter == 60) {
            achievementText.text = "You've collected all coins!";
        }
    }

    //Called every timestep
    void FixedUpdate()
    {
        Run();
        Jump();
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
        velocity.y = playerRigidbody.velocity.y;
        velocity.z = forwardInput * moveSettings.runVelocity;
        velocity.x = sidewaysInput * moveSettings.runVelocity;


        //Vector3 
        playerRigidbody.velocity = transform.TransformDirection(velocity);
        
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation = Quaternion.AngleAxis(moveSettings.rotateVelocity * turnInput * Time.deltaTime + cam.eulerAngles.y, Vector3.up);
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

        if(JumpCount==0&&IsGrounded()){
            JumpCount=MaxJumps;
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
            currentManager.damageSound.Play();
            Enemy enemy=collision.gameObject.GetComponent<Enemy>();
            Collider col=collision.gameObject.GetComponent<Collider>();
            Collider mycol=this.gameObject.GetComponent<Collider>();

               
           //TODO
           //Nochmal PDF angucken und Code überarbeiten, sonst gibt es hier ein Problem  mit bumpSpeed
            if(enemy.invincible)
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
                
                
        }

        if(collision.gameObject.CompareTag("Bomb")){
            currentManager.damageSound.Play();
            //Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Enemy enemy=collision.gameObject.GetComponent<Enemy>();
            Collider col=collision.gameObject.GetComponent<Collider>();
            Collider mycol=this.gameObject.GetComponent<Collider>();

               
           //TODO
           //Nochmal PDF angucken und Code überarbeiten, sonst gibt es hier ein Problem  mit bumpSpeed
            if(enemy.invincible)
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
                
                
        }

    }

    void Spawn(){
        //this.gameObject.transform.SetParent(spawnPoint);
        currentManager.ResetLives();
        transform.position=spawnPoint.position;
        if (speedBoosted) {
            speedBoostTimer = 6;
        }
        if (jumpBoosted) {
            jumpBoostTimer = 6;
        }
        for (int i = 0; i < boosterList.Count; i++) {
            boosterList[i].SetActive(true);
        }
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void OnDeath(){
        Spawn();
    }

/*    void OnCollisionExit(Collision collisionExit){

        
        if(collisionExit.gameObject.tag=="MovingPlatform"){
            platformRigidbody=null;
            isOnPlatform=false;
            collisionExit.gameObject.transform.parent=null;
        }
    }

  */  


    

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
            speedPickup.Play();
            if (speedBoosted) {
                speedBoostTimer = 0;
            } else {
                speedBoosted = true;
                moveSettings.runVelocity *= 1.5f;
            }
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            boosterList.Add(other.gameObject);
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }

        if (other.CompareTag("JumpBoost"))
        {
            jumpPickup.Play();
            if (jumpBoosted) {
                jumpBoostTimer = 0;
            } else {
                jumpBoosted = true;
                moveSettings.jumpVelocity *= 1.8f;
            }
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            boosterList.Add(other.gameObject);
            other.gameObject.SetActive(false);
           // Destroy(other.gameObject);
        }

        if (other.CompareTag("Collectible"))
        {
            coinPickup.Play();
            collectibleCounter++;
            GameData.Instance.Coins++;
            collectibleText.text = "Coins: " + collectibleCounter;
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        
    
        if (other.CompareTag("Key"))
        {
            keyPickup.Play();
            hasKey = true;
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            boosterList.Add(other.gameObject);
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }

        if (other.CompareTag("HealthKit"))
        {
            healPickup.Play();
            Instantiate(pickupEffect, other.transform.position, other.transform.rotation);
            currentManager.Heal(5);
            boosterList.Add(other.gameObject);
            other.gameObject.SetActive(false);
           // Destroy(other.gameObject);
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
