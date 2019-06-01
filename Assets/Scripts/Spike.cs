using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.x < -56) //Destroy once they are off camera
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Player Dies, main menu
            //BalloonController.playSound(SoundManager.SFX.pop);
            SnakeLoader.loadSnake(SnakeLoader.Scenes.BalloonScene);
            Destroy(gameObject);
        }
    }
}
