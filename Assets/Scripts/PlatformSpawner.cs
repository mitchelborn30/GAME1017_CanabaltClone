using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int startingPlatforms = 8;

    [Header("Spawn Distances")]
    [SerializeField] private float spawnAheadDistance = 30f;
    [SerializeField] private float destroyBehindDistance = 20f;

    [Header("Starting Position")]
    [SerializeField] private float firstPlatformX = 0f;
    [SerializeField] private float firstPlatformY = -3f;

    [Header("Gap Settings")]
    [SerializeField] private float minGap = 6f;
    [SerializeField] private float maxGap = 7f;
    [SerializeField] private int safeStartingPlatforms = 2;

    [Header("Height Settings")]
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 1f;
    [SerializeField] private float minYStep = -1f;
    [SerializeField] private float maxYStep = 1f;

    [Header("Platform Length Settings")]
    [SerializeField] private float minScaleX = 15f;
    [SerializeField] private float maxScaleX = 30f;

    [Header("Obstacle Settings")] 
    [SerializeField] private GameObject obstaclePrefab; 
    [SerializeField] private float obstacleChance = 0.4f; 

    private float basePlatformWidth;
    private float nextSpawnX;
    private float nextSpawnY;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private List<GameObject> spawnedObstacles = new List<GameObject>(); 

    private void Start()
    {
        basePlatformWidth = platformPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x / platformPrefab.transform.localScale.x;

        nextSpawnX = firstPlatformX;
        nextSpawnY = firstPlatformY;

        for (int i = 0; i < startingPlatforms; i++)
        {
            SpawnPlatform(i);
        }
    }

    private void Update()
    {
        if (player == null) return;

        while (nextSpawnX < player.position.x + spawnAheadDistance)
        {
            SpawnPlatform(spawnedPlatforms.Count);
        }

        RemoveOldPlatforms();
        RemoveOldObstacles(); 
    }

    private void SpawnPlatform(int platformIndex)
    {
        float randomScaleX = Random.Range(minScaleX, maxScaleX);
        float platformWidth = basePlatformWidth * randomScaleX;

        Vector3 spawnPos = new Vector3(nextSpawnX + platformWidth * 0.5f, nextSpawnY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        platform.transform.localScale = new Vector3(randomScaleX, platform.transform.localScale.y,
        platform.transform.localScale.z);

        spawnedPlatforms.Add(platform);

        if (platformIndex >= safeStartingPlatforms && obstaclePrefab != null && Random.value < obstacleChance) 
        {
            SpriteRenderer platformSR = platform.GetComponentInChildren<SpriteRenderer>(); 
            SpriteRenderer obstacleSR = obstaclePrefab.GetComponentInChildren<SpriteRenderer>(); 

            float obstacleX = platform.transform.position.x; 
            float obstacleY = platformSR.bounds.max.y + obstacleSR.bounds.extents.y; 

            GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(obstacleX, obstacleY, 0f), Quaternion.identity); 
            spawnedObstacles.Add(obstacle); 
        }

        float gapSize = Random.Range(minGap, maxGap);

        nextSpawnX += platformWidth + gapSize;

        if (platformIndex >= safeStartingPlatforms)
        {
            float randomYStep = Random.Range(minYStep, maxYStep);
            nextSpawnY += randomYStep;
            nextSpawnY = Mathf.Clamp(nextSpawnY, minY, maxY);
        }
    }

    private void RemoveOldPlatforms()
    {
        for (int i = spawnedPlatforms.Count - 1; i >= 0; i--)
        {
            if (spawnedPlatforms[i] == null)
            {
                spawnedPlatforms.RemoveAt(i);
                continue;
            }

            if (spawnedPlatforms[i].transform.position.x < player.position.x - destroyBehindDistance)
            {
                Destroy(spawnedPlatforms[i]);
                spawnedPlatforms.RemoveAt(i);
            }
        }
    }

    private void RemoveOldObstacles() 
    {
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--) 
        {
            if (spawnedObstacles[i] == null) 
            {
                spawnedObstacles.RemoveAt(i); 
                continue; 
            }

            if (spawnedObstacles[i].transform.position.x < player.position.x - destroyBehindDistance) 
            {
                Destroy(spawnedObstacles[i]); 
                spawnedObstacles.RemoveAt(i); 
            }
        }
    }
}