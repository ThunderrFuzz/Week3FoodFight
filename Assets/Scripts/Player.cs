using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 player features: 
    movement 
    interacts with food
    
 
 */
public class Player : MonoBehaviour
{
    [Header("Scripts")]
    public FoodThrow foodthrow;
    public float speed;
    public float throwVel;
    
    float hozInput;
    int health;
    public int maxHealth = 3;
    float verInput;

    [Header("Bounding Box vars")]
    public float xMax;
    public float xMin;
    public float zMax;
    public float zMin;

    [Header("Projectile Related")]
    public Animator playerAnim;
    public GameObject projectile;
    public GameObject[] projectiles;
    GameObject heldObject;
    public Vector3 projOffset;
    public float projectileLifespan;
    int ammo;
    public bool clickedOnFood;
    int score;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnim.enabled = true;
    }


  

    // Update is called once per frame
    void Update()
    {
       
        // apply gravity to player 

        //transform.Translate(Vector3.down * Time.deltaTime);

        hozInput = Input.GetAxis("Horizontal"); //x axis
        verInput = Input.GetAxis("Vertical"); //z axis

        //JUMPING 
        if (Input.GetKeyDown(KeyCode.Space) && !playerAnim.GetBool("Jump_b")) 
        {
            transform.Translate(Vector3.up); // notes about jump up is a little choppy 
            playerAnim.SetBool("Jump_b", true);  // animator jump 
        }
        if (transform.position.y > 0)
        {
            //moves player down
            transform.Translate(Vector3.down * 5 *  Time.deltaTime);
            movementLimiter();
            playerAnim.SetBool("Jump_b", false); // reset animator state 
            
        }
       

        //sets rotation based on move direction
        Vector3 movDir = new Vector3(hozInput, 0f, verInput).normalized; // normalized the vector of X and Z inputs 
        if (movDir.magnitude > 0.001f)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movDir, Vector3.up); // gets the desired rotation from move direction combined with the Y up vector 
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime); //takes the current rotation, and moves it to the desired rotation i 
        }
        if (Input.GetMouseButtonDown(1) && clickedOnFood)
        {
            //mouse 2 picks up 
            //
            
            

        }
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            //mouse 1 fires 
            //set held object to player pos and rotation 
            heldObject.transform.position = transform.position;
            heldObject.transform.rotation = transform.rotation;
            //enables heldobject again
            heldObject.gameObject.SetActive(true);
            
            
            //sets instance speed to 30 
            heldObject.GetComponent<FoodThrow>().speed = 45;
            Destroy(heldObject, projectileLifespan);
            ammo--;
            
        }
        

        /*
         code goes here for: if clicked on food, 
        instance heldobject at player postion  
        display heldobject
         */


      


        //teranry to determine running and walking speed 
        float walkspeed = Input.GetKey(KeyCode.LeftShift) ? speed * 2.5f : speed; // sets walkspeed based on shift running or not

        // moves left right, and handles animation state for walking running
        if (movDir.magnitude >= 0.1f) // checks if magnitude is more than anything
        {  
            transform.Translate(Vector3.forward * walkspeed * Time.deltaTime);  
            playerAnim.SetFloat("Speed_f", speed == walkspeed ? 0.3f : 0.6f); // walk or run speed of animation
            movementLimiter();
        } else {
            // play idle animtion 
            playerAnim.SetFloat("Speed_f", 0f);
        }
       
    }
    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.tag == "Animal" || col.gameObject.tag == "Dog")
        {
            setHealth(1);   
        }
    }
    public void addPoints(int points) {
        score += points;
        
    }
    public void setHealth(int dam) 
    { 
        health -= dam;
        
    }
    public int getPlayerHealth() { return health; }
    void movementLimiter()
    {
        // Sets current position to new position capping movement between given ranges

        if (transform.position.x >= xMax)
        {
            
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= xMin)
        {
            
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= zMax)
        {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
        }
        if (transform.position.z <= zMin)
        {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
        }

        if (transform.position.y < 0)
        {

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void setHeldFood(GameObject newFood)
    {
        heldObject = newFood;
    }
    public void addAmmo()
    {
        ammo++;
    }

    
}
