using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance; //The bridge between classes. I honestly probably could combine this with the SnakeManager.cs

    private void Awake()
    {
        instance = this;
    }

    public Sprite SnakeHeadSprite;
    public Sprite SnakeBodySprite1;
    public Sprite SnakeBodySprite2;
    public Sprite SnakeTailSprite;
    public Sprite FoodSprite;
    public Sprite DSprite;
    public Sprite GSprite;
    public Sprite SSprite;

    //Overkill, but easily expandable sound management.
    public SFXLinker[] soundsArray;
    [Serializable]
    public class SFXLinker
    {
        public SoundManager.SFX sound; //Enumerated Sound Type
        public AudioClip aClip; //The File it links to

    }
}
