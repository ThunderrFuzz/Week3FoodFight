using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AI : MonoBehaviour
{
    float currentHealth;
    int aiCount;
    int aiMax;
    public float maxHealth;
    GameObject currAI;
    public GameObject aiPrefab;
    public Player player;
    Gamemode gamemode;
    NavMeshAgent agent;
    public Transform playerPos;
    int speed;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gamemode = FindObjectOfType<Gamemode>();
        agent = FindObjectOfType<NavMeshAgent>();
        animator = FindObjectOfType<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed_f", 1f);
        }

        speed = Random.Range(3, 6);
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //moves

        if (agent.CompareTag("Dog"))
        {
            //finds all game objects tagged food
            GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
           
            // if the array has items within it
            if (foodObjects.Length  > 0)
            {
                //pick a random number
                int tar = Random.Range(0, foodObjects.Length );

                //set the target of the dog ai to the target foodobject 
                agent.destination = foodObjects[tar].transform.position;
                
            }
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); //moves
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Food")
        {
            Destroy(col.gameObject);
            gamemode.stolenFood++;
            gamemode.foodCount--;


        }
        
    }

    public void takeDamage(int dam)
    {
        //damages player 
        player.setHealth(dam);
    }
    public float getCurrHealth() { return currentHealth; }
    public void setCurrentHealth(int dam) { currentHealth = currentHealth - dam; } // damage to ai 

    
    
    
}
