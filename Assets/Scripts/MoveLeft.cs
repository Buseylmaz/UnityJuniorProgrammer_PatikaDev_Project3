using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20;
    float leftBound = -5;

    PlayerController playerControllerScript;

    /// <summary>
    /// Engelin ileri dogru hareket etmesi i�in
    /// </summary>

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        H�zArt();

        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if(transform.position.x < leftBound  &&  gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
        
        
    }

    void H�zArt()
    {
        if (Input.GetKey(KeyCode.S))
        {
            speed += 10;
            
        }
    }
}
