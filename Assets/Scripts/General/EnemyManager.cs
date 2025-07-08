using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoints
    {
        public Transform spawnPoint;
        public float maxX = 2f;
        public float minX = -2f;
        public float maxY = 2f;
        public float minY = -2f;
    }
    [SerializeField] SpawnPoints[] spawnPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int number = 5;
    [SerializeField] float maxTime, minTime;

    float spawnTime;

    bool canSpawn;
    private void Start()
    {
        RandomizeTime();
    }
    private void Update()
    {
        canSpawn = number > 0;

        if(canSpawn)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                SpawnEnemy();
            }
        }
    }
    void SpawnEnemy()
    {
        int index = Random.Range(0, enemies.Length);
        int pointIndex = Random.Range(0, spawnPoints.Length);

        //for offsetting
        float xOffset = Random.Range(spawnPoints[pointIndex].minX, spawnPoints[pointIndex].maxX);
        float yOffset = Random.Range(spawnPoints[pointIndex].minY, spawnPoints[pointIndex].maxY);

        Vector2 offset = new Vector2(xOffset, yOffset);
        GameObject sickler = Instantiate(enemies[index],offset + (Vector2)spawnPoints[pointIndex].spawnPoint.position, Quaternion.identity);
        number -= 1;
        RandomizeTime();
    }

    void RandomizeTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }
}
