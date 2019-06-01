using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid //Generally handles non-snake map elements.
{
    //Food Spawning
    private Vector2Int foodMapPosition;
    private Vector2Int DMapPosition;
    private Vector2Int GMapPosition;
    private Vector2Int SMapPosition;
    private int width;
    private int length;
    private GameObject thaFoood;
    private GameObject d;
    private GameObject g;
    private GameObject s;
    private Snake snake;

    public MapGrid(int length, int width) //Instantiate with size of Map
    {
        this.width = width;
        this.length = length;       
    }
    public void snekUp(Snake snek) //Link reference of Snake to here
    {
        this.snake = snek;
        FoodSpawner();
    }
    private void FoodSpawner()
    {
        do {
            foodMapPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, length)); //Random Position on the board.
        } while (snake.mapSpacesOccupied().IndexOf(foodMapPosition) != -1); //Don't spawn the food on your snake.

        thaFoood = new GameObject("Food", typeof(SpriteRenderer));
        thaFoood.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.FoodSprite;
        thaFoood.transform.position = new Vector3(foodMapPosition.x, foodMapPosition.y); //Set the food's location to the randomly generated MapPosition
        thaFoood.transform.localScale = new Vector3(4f, 4f, 0); //rescale to slightly smaller than snek head.
    }
    public bool SnekSneakSnekEat(Vector2Int snakeMapPosition) //Has the snake consumed the food?
    {
        if (snakeMapPosition == foodMapPosition)//eat the food
        {
            Object.Destroy(thaFoood); //Break eaten food
            FoodSpawner(); //make new food.
            SnakeManager.addScore();
            Debug.Log("Snake Ate Food, making more!");
            return true;
        }
        else
            return false;
    }
    public Vector2Int MapWrapper(Vector2Int headPosition) //Now you're thinking with portals.
    {
        if (headPosition.x < 0)
        {
            headPosition.x = width - 1;
        }
        else if(headPosition.x > width - 1)
        {
            headPosition.x = 0;
        }
        if (headPosition.y < 0)
        {
            headPosition.y = length - 1;
        }
        else if (headPosition.y > length - 1)
        {
            headPosition.y = 0;
        }
        return headPosition;
    }
    public bool spawnD()
    {
        do
        {
            DMapPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, length)); //Random Position on the board.
        } while (snake.mapSpacesOccupied().IndexOf(DMapPosition) != -1); //Don't spawn the food on your snake.

        d = new GameObject("D", typeof(SpriteRenderer));
        d.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.DSprite;
        d.transform.position = new Vector3(DMapPosition.x, DMapPosition.y); //Set the food's location to the randomly generated MapPosition
        d.transform.localScale = new Vector3(2f, 2f, 0); //rescale to slightly smaller than snek head.

        return true;
    }
    public bool spawnG()
    {
        do
        {
            GMapPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, length)); //Random Position on the board.
        } while (snake.mapSpacesOccupied().IndexOf(GMapPosition) != -1); //Don't spawn the food on your snake.

        g = new GameObject("G", typeof(SpriteRenderer));
        g.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.GSprite;
        g.transform.position = new Vector3(GMapPosition.x, GMapPosition.y); //Set the food's location to the randomly generated MapPosition
        g.transform.localScale = new Vector3(2f, 2f, 0); //rescale to slightly smaller than snek head.

        return true;
    }
    public bool spawnS()
    {
        do
        {
            SMapPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, length)); //Random Position on the board.
        } while (snake.mapSpacesOccupied().IndexOf(SMapPosition) != -1); //Don't spawn the food on your snake.

        s = new GameObject("S", typeof(SpriteRenderer));
        s.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.SSprite;
        s.transform.position = new Vector3(SMapPosition.x, SMapPosition.y); //Set the food's location to the randomly generated MapPosition
        s.transform.localScale = new Vector3(2f, 2f, 0); //rescale to slightly smaller than snek head.

        return true;
    }
    public char SnekLetter(Vector2Int snakeMapPosition) //Has the snake consumed the food?
    {
        if (snakeMapPosition == DMapPosition)//Collect D
        {
            Object.Destroy(d); //Break letter
            //FoodSpawner(); //make new food.
            SnakeManager.addScore();
            Debug.Log("Snake Ate letter D.");
            return 'D';
        }
        else if (snakeMapPosition == GMapPosition)//Collect G
        {
            Object.Destroy(g); //Break letter
            SnakeManager.addScore();
            Debug.Log("Snake Ate letter G.");
            return 'G';
        }
        else if (snakeMapPosition == SMapPosition)//Collect G
        {
            Object.Destroy(s); //Break letter
            SnakeManager.addScore();
            Debug.Log("Snake Ate letter S.");
            return 'S';
        }
        else
        {
            return 'N';
        }
            
    }
}
