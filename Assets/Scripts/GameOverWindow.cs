using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//I need to revisit this Script.
//Button works, but I cannot seem to get it to popup and disappear on its own; have tried several methods for it.
public class GameOverWindow : MonoBehaviour
{
    public Button button;
    public Button titleScreenButton;
    private static GameOverWindow instance;

    private void Awake()
    {
        instance = this;

        //button = transform.Find("retryButton").GetComponent<Button>();
        button.onClick.AddListener(retry);
        titleScreenButton.onClick.AddListener(title);

    }
    private void Start()
    {
        Hide(); //Shouldn't show unless you die.
    }
    public void retry()
    {
        Debug.Log("Scene being Reloaded!");
        //Reload Scene
        SnakeLoader.loadSnake(SnakeLoader.Scenes.SnakeGameScene);

    }
    public void title()
    {
        Debug.Log("Going back to title scene.");
        //To Main Menu
        SnakeLoader.loadSnake(SnakeLoader.Scenes.MainMenu);

    }
    private void Show()
    {
        button.gameObject.SetActive(true);
        titleScreenButton.gameObject.SetActive(true);
    }
    private void Hide()
    {
        button.gameObject.SetActive(false);
        titleScreenButton.gameObject.SetActive(false);
    }
    public static void showInstance()
    {
        instance.Show();
    }
}
