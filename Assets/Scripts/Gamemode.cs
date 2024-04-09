using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int maxFood = 35;
    int maxAI = 10;
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

        if(foodCount < maxFood)
        {
            int randomFood = Random.Range(0, foodPrefabs.Length -1); // rand number animal prefab chooser
            GameObject newSpawn = foodPrefabs[randomFood]; // sets the new spawn
            Vector3 spawnPos = spawnpoints[Random.Range(0, spawnpoints.Length -1)].position; // set spawn pos of new spawn
            foodprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns 
            foodCount++; // increase increment
           
        }
        
        /*if(thrown == true delete object -- food count)
        {
            foreach (var fooditem in foodPrefabs)
            {
                foodCount--;
                if (foodCount < 0) { foodCount = 0; }
                Destroy(foodprefab);
            }
        }*/



        if (spawnCount < maxAI)
        {
            
            int randomAnimal = Random.Range(0, animalPrefabs.Length - 1); // rand number animal prefab chooser
            GameObject newSpawn = animalPrefabs[randomAnimal]; // sets the new spawn
            Vector3 spawnPos = RandomSpawnpoint(); // set spawn pos of new spawn
            animalprefab = Instantiate(newSpawn, spawnPos, Quaternion.Euler(0f, 180f, 0f)); // spawns 
            spawnCount++; // increase increment
            if (gameObject.CompareTag("Dog"))
            {
                spawnCount--; // removes dogs from animal count allowing freeflowing animals in theroy and unlimited dogs.
            }
            if (animator != null)
            {
                animator.SetFloat("Speed_f", 1f);
            }
        }
        
        if (movementLimiter(animalprefab))
        { 
            foreach (var animal in animalPrefabs)
            {
                spawnCount--;
                if (spawnCount < 0) {  spawnCount = 0; }
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
        float randY = Random.Range(1f, 1f); // only need one y pos for all of them
        return new Vector3(randX, randY, randZ);
    }
}