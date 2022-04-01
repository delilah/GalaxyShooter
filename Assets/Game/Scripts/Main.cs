using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private bool _isGameRunning = false;

    void Update()
    {
        if (!_isGameRunning) 
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //start the game, so:
                //instantiate all prefabs
                // remove title screen
            }
        } 
    }

    public void EndGame()
    {
        // if player dies
        // stop spawning all the things
        // clear score
        // show title screen
    }








}
