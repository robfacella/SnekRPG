using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Delay During Scene Transition until the scene attempting to be loaded responds with an update.
public class LoaderDelay : MonoBehaviour
{
    private bool updated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updated == false)
        {
            updated = true;
            SnakeLoader.LoaderResume();
        }
    }
}
