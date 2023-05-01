using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
   // public GameObject enemyPrefab2;
    public int enemiesPerWave = 3;
    public float timeBetweenWaves = 10f;
    public Transform spawnPosition;
    
    public int enemiesRemaining = 0;
    public int currentWave = 0;

  
    public GameObject cocainePrefab;
    public Transform cocainespawnPosition;
    public float spawnInterval = 5f;
    private float timeSinceLastSpawn;
    
    public GameObject WindsPrefab;
    public Transform WindsspawnPosition;
    public float WindsspawnInterval = 5f;
    private float WindstimeSinceLastSpawn;
    
    void Start()
    {
        SpawnWave();
    }

    public void Update()
    {
     cocainSpawn();  
     WindsSpawn();
    }

    void SpawnWave()
    {
        currentWave++;
        enemiesRemaining = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 randomPosition = spawnPosition.position + new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f);
            int randomEnemyIndex = Random.Range(0, 2);

            if (randomEnemyIndex == 0)
            {
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab1, randomPosition, Quaternion.identity);
            }
        }
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;

        if (enemiesRemaining == 0)
        {
            StartCoroutine(WaitForNextWave());
        }
    }

    IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        SpawnWave();
    }

    public void cocainSpawn()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            Vector3 randomPosition = cocainespawnPosition.position + new Vector3(Random.Range(-2,2), Random.Range(-2,2), 0f);
            Instantiate(cocainePrefab, randomPosition, Quaternion.identity);
            timeSinceLastSpawn = 0f;
            spawnInterval = Random.Range(2, 10);
        }
    }
    
    public void WindsSpawn()
    {
        WindstimeSinceLastSpawn += Time.deltaTime;

        if (WindstimeSinceLastSpawn >= WindsspawnInterval)
        {
            Vector3 randomPosition = WindsspawnPosition.position + new Vector3(Random.Range(-2,2), Random.Range(-5,5), 0f);
            Instantiate(WindsPrefab, randomPosition, Quaternion.identity);
            WindstimeSinceLastSpawn = 0f;
            WindsspawnInterval = Random.Range(6, 10);
        }
    }

}

