using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 3;
    public float timeBetweenWaves = 10f;
    public Transform spawnPosition;
    
    public int enemiesRemaining = 0;
    public int currentWave = 0;

    void Start()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        currentWave++;
        enemiesRemaining = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 randomPosition = spawnPosition.position + new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
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
}

