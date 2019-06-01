using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BalloonController : MonoBehaviour
{
    private static BalloonController instance;
    //A few months ago one of my buddies offered to record "the speed metal version" of the NJIT theme song to use as background music.
    //Unfortunately those plans fell through. If this game is ever going to hit an App Store, having that song will be a prerequisite.
    private Vector2 newPosition;
    public float moveSpeed; //Set to 100 in editor.
    public float ScreenTopY; //12
    public float ScreenBotY; //-12
    private static string msg;

    private void Awake()
    {
        instance = this;
        msg ="Consuming GDS has left you terribly ill.";
    }
    // Start is called before the first frame update
    void Start()
    {
        newPosition = new Vector2(-24, 0);
        transform.position = newPosition;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position.y < ScreenTopY)) //Did we try to move up and won't go off screen?
        {
            newPosition = new Vector2(-25, transform.position.y + 12);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position.y > ScreenBotY))//Did we try to move Down and won't go off screen?
        {
            newPosition = new Vector2(-25, transform.position.y - 12);
        }
        transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime); //Move without snapping to the location immediately.

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reload Scene
            SnakeLoader.loadSnake(SnakeLoader.Scenes.SnakeGameScene);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Return to Title
            SnakeLoader.loadSnake(SnakeLoader.Scenes.MainMenu);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Quit Game        
            Debug.Log("Game is Exiting..");
            //Kill the Game
            Application.Quit();
        }
    }
    public static void setMsg(string newMsg)
    {
        msg = newMsg;
    }
    public static string getMsg()
    {
        return msg;
    }
    //Overkill, but easily expandable sound management.
    //public SFXLinker[] soundsArray;
    //[Serializable]
    //public class SFXLinker
    //{
    //   public SoundManager.SFX sound; //Enumerated Sound Type
    //   public AudioClip aClip; //The File it links to

    //}
}
