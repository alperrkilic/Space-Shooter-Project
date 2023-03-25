using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (int,float,bool,string)
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _speedMultiplier = 1.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _shieldVisualizer;

    //private bool _isSpeedBoostActive = false;
    private bool _isTripleShotActive = false;
    private bool _isShieldsActive = false;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;

// Start is called before the first frame update
void Start()
    {
        // Take the current position = new position (0,0,0) when starting
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); // accessing to spawn manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); // accessing to UI manager

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }


    }


    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if D pressed horizontal input=1, if A pressed input=-1 determining the direction
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //Constraints on the Movement
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <=-3.5)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        if(transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if(transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity); // Quaternion.Identity -> default rotation
        }

    }

    public void Damage()
    {

        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }


        _lives--; // if damaged, reduce life by one
        _uiManager.UpdateLives(_lives); // update ui lives


        if(_lives <= 0) // if player is dead, also spawning enemies must stop.
        {
            _spawnManager.OnPlayerDeath(); // set spawn variable to false
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive() // Public functions can be accessed from outside
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() // IEnumerator is a function to set timer called with StartCoroutine(function_name());
    {
        yield return new WaitForSeconds(5.0f); // 5 seconds of delay and after that setting bool variable to false
        _isTripleShotActive = false; // and controlling the activation time of the powerup
    }

    public void SpeedBoostActive()
    {
        //_isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        //_isSpeedBoostActive = false; // after 5 seconds, speed boost becomes inactive
        _speed /= _speedMultiplier;
    }


    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.gameObject.SetActive(true);
        StartCoroutine(ShieldBoostPowerDownRoutine());
    }

    IEnumerator ShieldBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualizer.gameObject.SetActive(false); // Shield visualizer becomes inactive after 5 seconds
        _isShieldsActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) // cooldown control
        {
            FireLaser();
        } 
    }
}
