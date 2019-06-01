using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    public Button play;
    public Button option;
    public Button instruction;
    public Button exit;
    public Button back; //From Instructions

    private enum SubMenu
    {
        Main,
        Instructions,
        //Options
    }
    private void Awake()
    {
        //play = transform.Find("PlayButton").GetComponent<Button>();
        transform.Find("Instructions").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("MainPage").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        play.onClick.AddListener(startSnake);
        option.onClick.AddListener(options);
        exit.onClick.AddListener(endGame);
        instruction.onClick.AddListener(instructions);
        back.onClick.AddListener(backToTitle);
        hideAll();
        showSubmenu(SubMenu.Main);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }
    public void startSnake()
    {
        Debug.Log("Game is being Started!");
        //Reload Scene
        SnakeLoader.loadSnake(SnakeLoader.Scenes.SnakeGameScene);

    }
    public void instructions()
    {
        Debug.Log("Tell them how to play!!");
        //Instructions Page
        showSubmenu(SubMenu.Instructions);


    }
    public void backToTitle()
    {
        Debug.Log("Main Menu..");
        //Clear and load Main Menu
        hideAll();
        showSubmenu(SubMenu.Main);

    }
    public void options()
    {
        Debug.Log("Show some options!");
        //Options Menu


    }
    public void endGame() //Spoilers?!
    {
        Debug.Log("Game is Exiting..");
        //Kill the Game
        Application.Quit();

    }
    private void hideAll()
    {
        //Add to as more submenus are added.
        transform.Find("Instructions").gameObject.SetActive(false);
        transform.Find("MainPage").gameObject.SetActive(false);
    }
    private void showSubmenu(SubMenu sub)
    {
        switch (sub)
        {
            case (SubMenu.Main):
                transform.Find("MainPage").gameObject.SetActive(true);
                break;
            case (SubMenu.Instructions):
                transform.Find("Instructions").gameObject.SetActive(true);
                break;
                //case (SubMenu.Options):
                //  transform.Find("Options").gameObject.SetActive(true);
                //break;

        }
    }
    private void UserInput()
    {
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
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            //Right Shift, skip to balloon debugger.
            SnakeLoader.loadSnake(SnakeLoader.Scenes.BalloonScene);
        }
    }
}
