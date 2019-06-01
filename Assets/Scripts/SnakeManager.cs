using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    private static SnakeManager instance;
    [SerializeField] private Snake snake;
    private MapGrid mGrid;
    private static int score;

    private void Awake() //Awake happens before Start
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SnakeManager.start();");

        mGrid = new MapGrid(20,20); //mGrid <MapGrid> contains the foodManager
        snake.griddyUp(mGrid);
        mGrid.snekUp(snake);

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int getScore()
    {
        return score;
    }
    public static void addScore()
    {
        score += 10;
    }
    public static void setScore(int newScore)
    {
        score = newScore;
    }
    public static void ripSnek()
    {
        GameOverWindow.showInstance();
    }
    public static void gds()
    {
        SnakeLoader.loadSnake(SnakeLoader.Scenes.BalloonScene);
    }
}
