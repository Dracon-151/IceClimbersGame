using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 3f;

    [SerializeField] private Transform level_part_0;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;
    [SerializeField] private Transform spawner;
    [SerializeField] public Transform despawnerinador;
    [SerializeField] private float spawnTime;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = level_part_0.Find("EndPosition").position;
        //Transform lastLevelPartTransform;
        
        int startingSpawnLevelParts = 0;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }

        StartCoroutine(SpawnTimer());
    }

    private void Update()
    {

    }

    private IEnumerator SpawnTimer()
    {
        SpawnLevelPart();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnTimer());
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        
    }
    private Transform SpawnLevelPart(Transform levelPart)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawner.position, Quaternion.identity);
        return levelPartTransform;
    }
}
