using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
//i doubt i need all of those namespaces. 

/*
 player features: 
    movement 
    interacts with food
    
 
 */
public class Player : MonoBehaviour
{
    public FoodThrow foodthrow;
    public float speed;
    public float throwVel;
    float hozInput;
    float health;
    public float maxHealth = 3;
    float verInput;
    public float xMax;
    public float xMin;
    public float zMax;
    public float zMin;
    public Animator playerAnim;
    public GameObject projectile;
    public GameObject[] projectiles;
    public Vector3 projOffset;
    public float projectileLifespan;
    int ammo;

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
        
        Quaternion desiredRotation = Quaternion.LookRotation(movDir, Vector3.up); // gets the desired rotation from move direction combined with the Y up vector 
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime); //takes the current rotation, and moves it to the desired rotation i 
        
        if (Input.GetMouseButtonDown(1))
        {
            //mouse 2 picks up 
            //
            ammo++;
            
        }
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            //mouse 1 fires projectile 
            //sets instance speed to 30 
           
            Vector3 spawnPos = transform.position + projOffset;
            int rand = Random.Range(0, projectiles.Length-1);
            GameObject projectileInstance = Instantiate(projectiles[rand], spawnPos, desiredRotation);
            ammo--;
            Destroy(projectileInstance, projectileLifespan);

            //grenade throw anim
            
        }

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
            Debug.Log("Game Lost");
        }
        


    }
    public void setHealth(int dam) { health -= dam; }
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
}
