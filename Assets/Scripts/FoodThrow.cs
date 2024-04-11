using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrow : MonoBehaviour
{
    // picks up spawned food items, and throws 
    // deal damage 
    // 

    public int speed;
    public Player player;
    public Animator animator;
    public Gamemode gamemode;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);   
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dog") || other.gameObject.CompareTag("Animal"))
        {
            //player.addPoints(5);
            player.hitAnimals += 5;
            Destroy(other.gameObject);
            gamemode.spawnCount--;
            
            //Debug.Log("added points for hitting animal." );
        }
    }

}
