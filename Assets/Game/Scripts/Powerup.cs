using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; //0 tripleshot, 1 speedboost, 2 shields

    [SerializeField]
    private AudioClip _clip;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < - 8f) 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (_clip != null) 
                {
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                }

                if (powerupID == 0) 
                {
                    player.TripleShotPowerUpOn();
                } 
                else if (powerupID == 1) 
                {
                    player.SpeedBoostPowerUpOn();
                }
                else if (powerupID == 2)
                {
                    player.EnableShields();
                }
            }

            Destroy(this.gameObject);
        }
    }

}
