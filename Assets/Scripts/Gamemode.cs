using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gamemode : MonoBehaviour
{
    // spawner of food items in progress 
    // spawner of enemies DONE ISH
    // points manager not done
    // 
    int totalScore;
    //arrays of objects
    public GameObject[] animalPrefabs;
    public GameObject[] foodPrefabs;
    public Transform[] spawnpoints;

    public Player player;
    public Animator animator;
    public int spawnCount;
    public int foodCount;
    public int stolenFood;
    int maxStolenFood = 45;
    int maxFood = 25;
    int maxAI = 10;

    NavMeshAgent agent;
    GameObject newSpawn;
    //single defintion for each item
    GameObject animalprefab;
    GameObject foodprefab;
    GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
      {
        if (player.getPlayerHealth() <= 0 || stolenFood >= maxStolenFood)
        {
            Debug.Log("Game Lost");
        }
        

        if (foodCount < maxFood )
          {
              int randomFood = Random.Range(0, foodPrefabs.Length); // rand number animal prefab chooser
              GameObject newSpawn = foodPrefabs[randomFood]; // sets the new spawn
              Vector3 spawnPos = spawnpoints[Random.Range(0, spawnpoints.Length)].position; // set spawn pos of new spawn
              foodprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns 
              foodCount++; // increase increment

          }

        if (spawnCount < maxAI)
        {
            int randomAnimal = Random.Range(0, animalPrefabs.Length); // rand number animal prefab chooser
            GameObject newSpawn = animalPrefabs[randomAnimal]; // sets the new spawn
            Vector3 spawnPos = RandomSpawnpoint(); // set spawn pos of new spawn
            animalprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns the object
            if (gameObject.CompareTag("Dog"))
            {
                // removes dogs from animal count allowing freeflowing animals in theroy and unlimited dogs. dogs despawn after 5s or until dead
                Destroy(gameObject, 55);
                spawnCount--;
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




        if (movementLimiter(animalprefab))
          { 
              foreach (var animalprefab in animalPrefabs)
              {
                  spawnCount--;
                  
                  Destroy(animalprefab);
              }
          }

      }
    

   
    public void AddPoints(int pointstoadd)
    {
        totalScore += pointstoadd;
    }
    bool movementLimiter(GameObject animal)
    {
        // Sets current position to new position capping movement between given ranges


        if (animal.transform.position.z < player.zMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.zMin);
            return true;
        }
        if (animal.transform.position.x < player.xMin)
        {
            transform.position = new Vector3(player.xMin, transform.position.y, transform.position.z );
            return true;
        }
        if(animal.transform.position.x > player.xMax)
        {
            transform.position = new Vector3(player.xMax, transform.position.y,  transform.position.z);
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

