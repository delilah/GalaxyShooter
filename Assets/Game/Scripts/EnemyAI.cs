using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

  [SerializeField] private GameObject _enemyExplosionPrefab;
  [SerializeField] private float _speed = 2f;
  [SerializeField] private GameObject _enemyPrefab;
  [SerializeField] private AudioClip _clip;

  private UIManager _uiManager;

    

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f) 
        {
            SpawnEnemyAtRandomPos();
        }
        
    }

    private void SpawnEnemyAtRandomPos() 
    {
        float randomX = Random.Range(-10f, 10f);
        transform.position =  new Vector3(randomX, 8f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            // Destroy(other.gameObject);
        }
        
    }

    private void DestroyAndRespawn()
    {
        // Destroy(this.gameObject);
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-10f, 10f), 8f, 0), Quaternion.identity);
    }

}
