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
    [SerializeField] private float minGap = 1f;
    [SerializeField] private float maxGap = 2.5f;
    [SerializeField] private int safeStartingPlatforms = 2;

    [Header("Height Settings")]
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 1f;
    [SerializeField] private float minYStep = -1f;
    [SerializeField] private float maxYStep = 1f;

    private float platformWidth;
    private float nextSpawnX;
    private float nextSpawnY;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private void Start()
    {
        platformWidth = platformPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

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
    }


    private void SpawnPlatform(int platformIndex)
    {
        Vector3 spawnPos = new Vector3(nextSpawnX, nextSpawnY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        platform.transform.localScale = new Vector3(Random.Range(15f, 30f), 6f, 1f);
        float platformWidth = platform.GetComponentInChildren<SpriteRenderer>().bounds.size.x;


        spawnedPlatforms.Add(platform);

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
}