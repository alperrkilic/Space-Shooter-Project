using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    // Create a coroutineof type IEnumerator -- Yield Events
    
    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity); // Quaternion.identity stands for default rotation settings
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.5f);
        }
    }

}
