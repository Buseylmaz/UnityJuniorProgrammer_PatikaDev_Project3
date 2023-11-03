using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    float stratDelay = 1.3f;
    float repeatRate = 1.3f;

    PlayerController playerControllerScript;
   
    void Start()
    {
       
        InvokeRepeating("ObstacleSpawner", stratDelay, repeatRate);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void ObstacleSpawner()
    {
        Vector3 spawnPos = new Vector3(42, 0, 0);

        int obstacle = Random.Range(0, obstaclePrefabs.Length);


        //eðer gameover olmadýysa spaw yapsýn ama gameover olursa spaw yapmayý durdursun 
        if (playerControllerScript.gameOver == false)
        {
           Instantiate(obstaclePrefabs[obstacle], spawnPos, obstaclePrefabs[obstacle].transform.rotation);
        }
        
    }


}
