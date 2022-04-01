using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldActive = false;

    public int numberOfLives = 3;

    [SerializeField]
    private GameObject _laserPrefab;
     [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    
    [SerializeField]
    private float _speedMultiplier = 2f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private int _hitCount = 0;



    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager!= null) 
        {
            _uiManager.UpdateLives(numberOfLives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


          if (_spawnManager!= null) 
        {
            _spawnManager.StartSpawning();
        }

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
           Shoot();
        }
    }

    private void Shoot() 
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();


            if (canTripleShot) {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            } else 
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, .88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive) 
        {
            transform.Translate(Vector3.right * _speed * _speedMultiplier * horizontalInput * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * _speed * _speedMultiplier * verticalInput * Time.deltaTime);
        } 
        else
        {
            transform.Translate(Vector3.right * _speed *  horizontalInput * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * _speed * verticalInput * Time.deltaTime);
        }

        

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
        }

        // Left and Right Bounds
        // if (transform.position.x > 8.1f) {
        //     transform.position = new Vector3(8.1f, transform.position.y, transform.position.z);
        // }
        // else if (transform.position.x < -8.1f) {
        //     transform.position = new Vector3(-8.1f, transform.position.y, transform.position.z);
        // }

        // Left and Right Bounds, re-appears on other side
        if (transform.position.x > 10.8f)
        {
            transform.position = new Vector3(-10.8f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -10.8f)
        {
            transform.position = new Vector3(10.8f, transform.position.y, transform.position.z);
        }

    }

    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }


        _hitCount++;

        if (_hitCount == 1) 
        {
            _engines[0].SetActive(true);
        } 
        else if (_hitCount == 2) 
        {
            _engines[1].SetActive(true);
        }

        numberOfLives--;
        _uiManager.UpdateLives(numberOfLives);

        if (numberOfLives <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    public void EnableShields()
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void TripleShotPowerUpOn() 
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerUpOn() 
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownroutine());
    }


    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownroutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

}
