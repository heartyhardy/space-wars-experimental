using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> WaveConfigs;
    [SerializeField] bool looping = true;

	// Use this for initialization
	IEnumerator Start () {

        do
        {
            yield return StartCoroutine(SpawnAllWaves(WaveConfigs));
        }
        while (looping);

	}

    private IEnumerator SpawnAllWaves(List<WaveConfig> waveConfigs)
    {
        Coroutine currentCoroutine;

        foreach(WaveConfig waveConfig in waveConfigs)
        {
            currentCoroutine = StartCoroutine(SpawnAllEnemiesInWave(waveConfig));
            yield return currentCoroutine;
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i=0; i<waveConfig.GetEnemyCount();i++)
        {
            var spawned= Instantiate(
                    waveConfig.GetEnemy(),
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity
                );
            spawned.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnTime());
        }
    }
}
