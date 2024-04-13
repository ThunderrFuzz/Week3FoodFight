using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class Despawner : MonoBehaviour
{
    Gamemode gamemode;
    void Start()
    {
        gamemode = FindObjectOfType<Gamemode>();    
    }
    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Dog"|| col.tag == "Animal") && col.tag == "Despawner")
        {
            Destroy(col.gameObject);
            gamemode.spawnCount--;
        }

    }
   

}
