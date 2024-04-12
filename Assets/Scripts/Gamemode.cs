using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Gamemode : MonoBehaviour
{
    // spawner of food items in progress 
    // spawner of enemies DONE ISH
    // points manager not done
    // 
    
    //arrays of objects
    public GameObject[] animalPrefabs;
    public GameObject[] foodPrefabs;
    public Transform[] spawnpoints;

    public Player player;
    public Animator animator;
    public int spawnCount;
    public int foodCount;
    public int stolenFood;
    int maxStolenFood = 150;
    int maxFood = 125;
    int maxAI = 35;

    float timeLimit = 60f; // Set the time limit to 60 seconds
    float currentTime; // Current time remaining
    float remainingTime;
    public bool gameLost;

    NavMeshAgent agent;
    GameObject newSpawn;
    //single defintion for each item
    GameObject animalprefab;
    GameObject foodprefab;
    GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        
        gameLost = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        

       

        if (gameLost)
        {
            Debug.Log("Game Lost");
            
            player.enabled = false;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
                player.enabled = true;
            }
        }
        else
        {
            lossConditions();
            player.enabled = true;
        }
        //spawn food
        if (foodCount < maxFood)
        {
            int randomFood = Random.Range(0, foodPrefabs.Length); // rand number animal prefab chooser
            GameObject newSpawn = foodPrefabs[randomFood]; // sets the new spawn
            Vector3 spawnPos = spawnpoints[Random.Range(0, spawnpoints.Length)].position; // set spawn pos of new spawn
            foodprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns 
            foodCount++; // increase increment

        }

       

        //spawn animals
        if (spawnCount < maxAI)
        {

            int randomAnimal = Random.Range(0, animalPrefabs.Length); // rand number animal prefab chooser
            GameObject newSpawn = animalPrefabs[randomAnimal]; // sets the new spawn
            Vector3 spawnPos = RandomSpawnpoint(); // set spawn pos of new spawn
            animalprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns the object
            if (animalprefab.CompareTag("Dog"))
            {
                // removes dogs from animal count allowing freeflowing animals in theroy and unlimited dogs. dogs despawn after 5s or until dead
                Destroy(animalprefab, 10);
                
            }
            else
            {
                spawnCount++;

            }
            if (animator != null)
            {
                animator.SetFloat("Speed_f", 1f);
                // increase increment of spawn count only for animals

            }
        }
        if (movementLimiter(newSpawn))
        {
            foreach (var animal in animalPrefabs)
            {
                Destroy(animalPrefab);
                spawnCount--;


            }
        }







    }

    void lossConditions()
    {
        // Check for game loss conditions
        if (player.getPlayerHealth() <= 0 || stolenFood >= maxStolenFood || currentTime >= timeLimit)
        {
            // Set gameLost to true if any of the conditions are met
            Debug.Log("Game Lost");
            gameLost = true;
        }
        else
        {
            // Calculate the remaining time if the game is not lost
            remainingTime = timeLimit - currentTime;

            // Calculate the score based on the player's health, stolen food, hit animals, and remaining time
            if (stolenFood != 0)
            {
                int scoreIncrease = Mathf.FloorToInt((player.getPlayerHealth() / stolenFood) * remainingTime + player.hitAnimals);
                // Update the player's score
                player.score += scoreIncrease;
            }
           
        }
    }



    bool movementLimiter(GameObject animal)
    {
        // Sets current position to new position capping movement between given ranges


        if (animal.transform.position.z < player.zMin)
        {
            
            return true;
        }
        if (animal.transform.position.x < player.xMin)
        {
            
            return true;
        }
        if(animal.transform.position.x > player.xMax)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 RandomSpawnpoint()
    {
        float randX = Random.Range(player.xMax, player.xMin); ;
        float randZ = Random.Range(player.zMax, player.zMax + 59); ;
        float randY = Random.Range(0f, 1f); // only need one y pos for all of them
        return new Vector3(randX, randY, randZ);
    }
    


}

