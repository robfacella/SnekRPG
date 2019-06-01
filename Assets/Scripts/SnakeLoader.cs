using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SnakeLoader //Scene Management
{
    public enum Scenes
    {
        SnakeGameScene,
        LoadingScreen,
        MainMenu,
        BalloonScene
    }
    private static Action loaderReadyAction;

    public static void loadSnake(Scenes scene)
    {
        loaderReadyAction = () =>
        {
            //Load Desired scene after load screen has updated.
            SceneManager.LoadScene(scene.ToString());
        };


        //Display loading screen...
        SceneManager.LoadScene(Scenes.LoadingScreen.ToString());
    }
    public static void LoaderResume() //Called to from the load screen scene
    {
        if (loaderReadyAction != null)
        {
            loaderReadyAction();
            loaderReadyAction = null;
        }
        else //If something, somehow, breaks: Just go to main menu by default.
        {
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
        }
    }
}
