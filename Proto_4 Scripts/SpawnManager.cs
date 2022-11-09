using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9;
    public int enemyCount = 0;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyManager(waveNumber);
        Instantiate(powerupPrefab, GeneratePosition(), powerupPrefab.transform.rotation);
    }
    void SpawnEnemyManager(int enemiestoSpawn)
    {
        for(int i = 0; i < enemiestoSpawn; i ++)
        {
            Instantiate(enemyPrefab, GeneratePosition(), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyManager(waveNumber);
            Instantiate(powerupPrefab, GeneratePosition(), powerupPrefab.transform.rotation);
        }
    }
    private Vector3 GeneratePosition()
    {
        float randomPosX = Random.Range(-spawnRange, spawnRange);
        float randomPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(randomPosX, 0, randomPosZ);
        return randomPos;
    }
}
