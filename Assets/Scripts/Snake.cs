using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private enum Direction{
        Up,
        Down,
        Left,
        Right
    }
    private enum Status
    {
        Alive,
        Dead
    }
    private bool spawnedG;
    private bool spawnedD;
    private bool spawnedS;//All Spawn fine, need to collect in order...
    private bool collectG;//Char list instead?..
    private bool collectD;
    private bool collectS;
    private Status state;
    private Vector2Int gridPosition;
    private Direction snekDirection;
    private float crawlTimer;   //Time Until Next Movement
    private float timeTillMove; //Interval For Movement
    private MapGrid mapGrid;
    private int snekSize;
    private List<SnakeTransformer> snekLocationHistory;
    private List<SnekParts> snakeBodyList;
    private List<char> lettersCollectedList;
    
    

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        //timeTillMove = 1f; //1f ~ 1sec
        timeTillMove = .5f; // ~ 1/2 sec
        crawlTimer = timeTillMove;
        snekDirection = Direction.Right;
        snekSize = 0;
        snekLocationHistory = new List<SnakeTransformer>();
        snakeBodyList = new List<SnekParts>();
        updateSnekParts();
        state = Status.Alive;

        spawnedG = false;
        spawnedD = false;
        spawnedS = false;
        lettersCollectedList = new List<char>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void griddyUp(MapGrid mGrid) //Reference for MapGrid
    {
        this.mapGrid = mGrid;
    }

    // Update is called once per frame
    void Update()
    {
        UserInput(); //Need some user input even when dead to restart.
        switch (state)
        {
            case Status.Alive:
                SnekMovement();
                break;
            case Status.Dead:
                break;
        }

    }
    private void UserInput()
    {
        //Leave 180 turn option as kill.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            snekDirection = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            snekDirection = Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            snekDirection = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            snekDirection = Direction.Left;
        }
        /////////////////////////////////////////
        ///Menu Buttons//////////////////////////
        /////////////////////////////////////////
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
    private void SnekMovement()
    {
        crawlTimer += Time.deltaTime; //Used for snake movement interval
        if (crawlTimer >= timeTillMove) //If snake needs to move, then move
        {
            SnakeTransformer last = null; //Failsafe for first body segment
            if (snekLocationHistory.Count > 0)
            {
                last = snekLocationHistory[0]; //Prior Segment if avaialable, else null
            }
            SnakeTransformer snekLocation = new SnakeTransformer(last, gridPosition, snekDirection); //Class (and "last" parameter) used for properly rotaing body segments.
            Vector2Int director; //Used in determining next location of snake.
            switch (snekDirection)
            {
                default:
                case Direction.Right: director = new Vector2Int(+1, 0); break;
                case Direction.Left: director = new Vector2Int(-1, 0); break;
                case Direction.Up: director = new Vector2Int(0, +1); break;
                case Direction.Down: director = new Vector2Int(0, -1); break;
            }
            //updateSnekParts();
            snekLocationHistory.Insert(0, snekLocation); //Add current location to front of list.
            gridPosition += director; //Needed location added before this, else included head tile as first body.
            gridPosition = mapGrid.MapWrapper(gridPosition); //If the snake went through a border, return at opposite border. (same velocity)
            crawlTimer -= timeTillMove; //Reset Tick of timer. Game ticks.
        
            //Body Tracker////
            bool snakeAte = mapGrid.SnekSneakSnekEat(gridPosition);//Let Map know where Snekky is for food collection check
            if (snakeAte)
            {
                //Grow Snake
                snekSize++;
                generateBodySegment(); //Adds the body segment.
                SoundManager.playSound(SoundManager.SFX.pop); //Plays a sound effect.
            }
            //ReUse snakeAte for secret test
            if ((snekSize > 5) && (spawnedD == false))
            {
                spawnedD = mapGrid.spawnD();

            }
            if ((snekSize > 4) && (spawnedG == false))
            {
                spawnedG = mapGrid.spawnG();

            }
            if ((snekSize > 3) && (spawnedS == false))
            {
                spawnedS = mapGrid.spawnS();

            }

            //char snakeLetterTemp = mapGrid.SnekLetter(gridPosition);//Let Map know if Snake collected G D or S
            if(lettersCollectedList.Count < 3)
            {
                //If not all letters were collected yet.
                //Check if Snake is eating any Letters.
                //if so return which
                char snakeLetterTemp = mapGrid.SnekLetter(gridPosition);//Let Map know if Snake collected G D or S
                //Returns 'N' if no.
                if (snakeLetterTemp != 'N')
                {
                    lettersCollectedList.Add(snakeLetterTemp);
                }
                //Else ya didn't get a letter, lol.

            }
            else if(lettersCollectedList.Count == 3)
            {
                //Once all letters have been collected
                //Check if letters are in order; enable button.
                if(lettersCollectedList[0] == 'G')
                {
                    if (lettersCollectedList[1] == 'D')
                    {
                        //Therefore [2] is 'S'
                        //Enable Level Warp Button.. or instant...

                        SnakeManager.gds();
                    }
                    else
                    {
                        //Hint that first was right.
                    }
                }
                //<Else>/<Always> add an arbitrary char to the  lettersCollectedList
                //Always
                lettersCollectedList.Add('N');

            }
            else
            {
                //Hacks //or add something to list if the letters weren't in the right order, thus preventing game from loading code within count == 3 logic repeatedly each update after...

            }

            if (snekLocationHistory.Count >= (snekSize + 1)) //<if> We are storing more locations than the total body size of the snake
            {
                snekLocationHistory.RemoveAt((snekLocationHistory.Count -1)); //<then> Trim fat, ya didn't need to store so much snek.
            }
            //////////////////

            transform.position = new Vector3(gridPosition.x, gridPosition.y); //Move Head
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(director) -90); //Minus 90 fixes original rotation
            updateSnekParts(); //Do not check for Death before this Update
            ////Check for Death

            foreach (SnekParts bodypart in snakeBodyList) //Have we killed the snek???
            {
                Vector2Int bodyMapPosition = bodypart.getMapPos();
                if (gridPosition == bodyMapPosition) //If the head overlaps any bodypart
                {
                    //GameOver
                    //Debug.Log("Snake has Died!! :c"); //Commented out after popup retry working
                    state = Status.Dead;
                    SnakeManager.ripSnek();

                }

            }
            //////////////////
        }
    }
    private float GetAngleFromVector (Vector2Int snakeDirection) //Snake Head Rotation
    {
        float snakeHead = Mathf.Atan2(snakeDirection.y, snakeDirection.x) * Mathf.Rad2Deg;
        if (snakeHead < 0)
        {
            snakeHead += 360;
        }
        return snakeHead;//Snake Head Rotation
    }
    public Vector2Int GetHeadGridPos() //Public Accessoror for the current location of Snake's head on grid.
    {
        return gridPosition; //An otherwise private Var
    }
    public List<Vector2Int> mapSpacesOccupied() //Returns all spaces which the snake currently occupies
    {
        List<Vector2Int> allSlitheredSpaces = new List<Vector2Int>() { gridPosition };//head
        foreach(SnakeTransformer item in snekLocationHistory) //For each body part being tracked as part of the snake.
        {
            allSlitheredSpaces.Add(item.getMapPosition());//body space(s)
        }
        
        return allSlitheredSpaces; //Returned to MapGrid.cs //Used for determining where NOT to spawn the fruit.
    }
    private void generateBodySegment() //Creates the new SnakeBodyPart GameObjects and adds them to the list.
    {
        snakeBodyList.Add(new SnekParts(snakeBodyList.Count));
    }
    private void updateSnekParts()
    {
        for (int i = 0; i < snakeBodyList.Count; i++) //Body Segment 'Movement'
        {
            //Vector3 snekBodyLocation = new Vector3(snekLocationHistory[i].x, snekLocationHistory[i].y);
            snakeBodyList[i].setSnakeMapPosition(snekLocationHistory[i]);//Moves body segments to their new respective locations.
        }
    }
    private class SnekParts //Generates and Handles the segments of the snake.
    {
        private SnakeTransformer snakePosition;
        private Transform transform;
        public SnekParts(int indexOfSegment)
        {
            GameObject snakeBodyGO = new GameObject("SnakeBodySegment", typeof(SpriteRenderer));

            if (indexOfSegment % 3 == 0)//Every third body segment has an altered texture.
            {                           //A slight difference but refreshing.
                snakeBodyGO.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.SnakeBodySprite2;
            }                           //Also makes it easier to view that the segments are persistently moving with the snake.
            else { 
                snakeBodyGO.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.SnakeBodySprite1;
            }
            snakeBodyGO.GetComponent<SpriteRenderer>().sortingOrder = -indexOfSegment-1; //Places body sgments under each other. Head on top, tail low.
            transform = snakeBodyGO.transform;  //Don't forget to instantiate.
            transform.localScale = new Vector3(2.8f, 2.8f, 0); //rescale to slightly smaller than snek head.
        }

        public void setSnakeMapPosition(SnakeTransformer snakePosition)
        {
            this.snakePosition = snakePosition;
            transform.position = new Vector3(snakePosition.getMapPosition().x, snakePosition.getMapPosition().y);

            //Use Direction to set ANGLE for transform. //Alligns body segments based on direction of travel and cornering the segments.
            float angle;
            switch (snakePosition.getDirection())
            {
                default:
                case Direction.Right:
                    switch (snakePosition.getPriorDirection())
                    {
                        default:
                            angle = 90;
                            break;
                        case Direction.Up: //Last was up
                            angle = 90+45;
                            break;
                        case Direction.Down: //Last was down
                            angle = 90-45;
                            break;
                    }
                    break;
                case Direction.Up:
                    switch (snakePosition.getPriorDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case Direction.Right: //Last was Right
                            angle = -45;
                            break;
                        case Direction.Left: //Last was Left
                            angle = 45;
                            break;
                    }
                    break;
                case Direction.Left:
                    switch (snakePosition.getPriorDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Up: //Last was up
                            angle = -90 - 45;
                            break;
                        case Direction.Down: //Last was down
                            angle = -90 + 45;
                            break;
                    }
                    break;
                case Direction.Down:
                    switch (snakePosition.getPriorDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case Direction.Right: //Last was Right
                            angle = 180+45;
                            break;
                        case Direction.Left: //Last was Left
                            angle = 180-45;
                            break;
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        public Vector2Int getMapPos()
        {
            return snakePosition.getMapPosition();
        }
    }
    private class SnakeTransformer //Used for Rotation of Snake Body Segments in relation to the previous segment.
    {
        private Vector2Int mapPos;
        private SnakeTransformer lastSnakeTransform;
        private Direction direction; //Up, Down, Left, Right

        public SnakeTransformer(SnakeTransformer lastSnakeTransformer, Vector2Int mapPos, Direction direction)
        {
            this.lastSnakeTransform = lastSnakeTransformer;
            this.mapPos = mapPos;
            this.direction = direction;
        }
        public Vector2Int getMapPosition()
        {
            return mapPos;
        }
        public Direction getDirection()
        {
            return direction;
        }
        public Direction getPriorDirection()
        {
            if (lastSnakeTransform == null)
            {
                return Direction.Up; // return anything for first segment, prevent error
            }
            else
            {
                return lastSnakeTransform.direction;
            }
        }
    }
}

