using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float who_let_the_dog_out;
    private float lastSpawnTime;

    // Update is called once per frame
    private void Start()
    {
        // give last spawn time a value to enable "shooting" at start
        lastSpawnTime = -who_let_the_dog_out;
    }
    void Update()
    {
        if (Time.time - lastSpawnTime >= who_let_the_dog_out)
        {
            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                lastSpawnTime = Time.time;
            }
        }
    }
}
