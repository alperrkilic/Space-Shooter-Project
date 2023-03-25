using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject [] powerups;
    [SerializeField]
    private float _EnemySpawnTime = 3.5f;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    // Create a coroutineof type IEnumerator -- Yield Events
    
    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning == false)
        {
            if (_EnemySpawnTime >= 1.2f)
            {
                _EnemySpawnTime -= 0.1f;
            }
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity); // Quaternion.identity stands for default rotation settings
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_EnemySpawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0); // generating a random position for spawn
            int randomPowerUp = Random.Range(0,3); // generating a random number betweeen 1-3 so we can call a random powerup
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity); // instantiating the object at random spawn with default rotation
            yield return new WaitForSeconds(Random.Range(12, 21)); // in 12 to 20 seconds (20 included) new powerup will be spawned
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true; // if player is dead, stop spawning new enemies and powerups
    }

}
