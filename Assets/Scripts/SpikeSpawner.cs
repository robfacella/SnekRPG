using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spike;
    public GameObject spike2;
    private float timeTillSpawn;
    public float timeBetweenSpawns;
    public float speedUpBy;
    public float quickestSpawn;
    private Vector2 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTillSpawn <= 0)
        {
            newPosition.x = transform.position.x;
            
            //Middle 3
            //High 15
            //Low -9
            int r = Random.Range(0, 3); //0,1,2
            if (r == 0) //Middle Gap
            {
                newPosition.y = -9;
                Instantiate(spike, newPosition, Quaternion.identity);
                newPosition.y = 15;
                Instantiate(spike2, newPosition, Quaternion.identity);
            }
            else if (r == 1) //Top Gap
            {
                newPosition.y = -9;
                Instantiate(spike, newPosition, Quaternion.identity);
                newPosition.y = 3;
                Instantiate(spike2, newPosition, Quaternion.identity);
            }
            else if (r == 2)//Bot Gap
            {
                newPosition.y = 15;
                Instantiate(spike, newPosition, Quaternion.identity);
                newPosition.y = 3;
                Instantiate(spike2, newPosition, Quaternion.identity);
            }
            else
            {
                ///Hacks.
                timeBetweenSpawns = 0;
            }


            ////////////////////////////////////
            timeTillSpawn = timeBetweenSpawns;
            if (timeBetweenSpawns > quickestSpawn)
            {
                timeBetweenSpawns -= speedUpBy;
            }
            if(timeBetweenSpawns < 5 && timeBetweenSpawns > 4)
            {
                //What you want a medal?
                BalloonController.setMsg("Well what did you really expect anyway?");

            }
            if (timeBetweenSpawns < 4.5)
            {
                //Ok, We're impressed.
                BalloonController.setMsg("[Esc] While you still can.");
                //This is where the next transition key would be.
            }
            if (timeBetweenSpawns < 4)
            {
                //OMG, stop already, it's not that interesting.
                BalloonController.setMsg("You win, I suppose... {Press [Esc] or [r] or [q]}");
            }
        }
        else
        {
            timeTillSpawn -= Time.deltaTime;
        }
    }
}
