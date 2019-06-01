using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Directly touches the Score Text Object, but this is itself controlled through SnakeManager.cs
public class ScorePanel : MonoBehaviour
{
    private Text scoreText;
    private void Awake()
    {
        scoreText = transform.Find("ScoreText").GetComponent<Text>();        
    }
    private void Update()
    {
        scoreText.text = SnakeManager.getScore().ToString();
    }
}
