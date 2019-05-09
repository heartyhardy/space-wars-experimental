using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject path;
    [SerializeField] float spawnTime=0.5f;
    [SerializeField] float spawnRandomness=0.3f;
    [SerializeField] int enemyCount=5;
    [SerializeField] float moveSpeed=3f;

    public GameObject GetEnemy()
    {
        return enemy;
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in path.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }

    public float GetSpawnRandomness()
    {
        return spawnRandomness;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}

