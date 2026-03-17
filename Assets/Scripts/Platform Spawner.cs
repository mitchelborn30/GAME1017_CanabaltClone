using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int startingPlatforms = 8;

    [SerializeField] private float spawnAheadDistance = 30f;
    [SerializeField] private float destroyBehindDistance = 20f;
    [SerializeField] private float spawnY = -10f;

    [Header("Gap Settings")]
    [SerializeField] private float minGap = 30f;
    [SerializeField] private float maxGap = 44f;
    [SerializeField] private float chanceOfGap = 0.99f;

    private float platformWidth;
    private float nextSpawnX = 18f;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private void Start()
    {
        platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 0; i < startingPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        if (player == null) return;

        while (nextSpawnX < player.position.x + spawnAheadDistance)
        {
            SpawnPlatform();
        }

        RemoveOldPlatforms();
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3(nextSpawnX, spawnY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        spawnedPlatforms.Add(platform);

        float gapSize = 0f;

        if (Random.value < chanceOfGap)
        {
            gapSize = Random.Range(minGap, maxGap);
        }

        nextSpawnX += platformWidth + gapSize;
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