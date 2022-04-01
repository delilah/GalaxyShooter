using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager _uiManager;
    // private SpawnManager _spawnManager;


    private void Start() 
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        // _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    private void Update() 
    {

        if (gameOver) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
                // _spawnManager.StartSpawning();
            }  
        
            
        }
        
    }

    


}
