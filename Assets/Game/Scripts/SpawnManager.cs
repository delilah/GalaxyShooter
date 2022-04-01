using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups;

    //  IEnumerator EnemySpawnCO;
    // IEnumerator PowerUpSpawnCO;

    private GameManager _gameManager;


    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // EnemySpawnCO = EnemySpawnRoutine();
        // PowerUpSpawnCO = PowerupSpawnRoutine();

    }

    // public void StopSpawning() 
    // {
    //     Debug.Log("StopSpawning");

    //     StopCoroutine(EnemySpawnCO);
    //     StopCoroutine(PowerUpSpawnCO);
    // }

    public void StartSpawning() 
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while(!_gameManager.gameOver)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-10f, 10f), 8f, 0), Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        } 
    }

    IEnumerator PowerupSpawnRoutine()
    {

        while(!_gameManager.gameOver)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(-10f, 10f), 8f, 0), Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
        
    }

}
