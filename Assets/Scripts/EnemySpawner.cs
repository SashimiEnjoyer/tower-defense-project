using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Wave
{
    public int amountFirstSpawn;
    public List<GameObject> list;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] wave;
    [SerializeField] Transform[] spawnPoints;

    private int spawnTracker = 0;
    private List<GameObject> currentEnemyWave = new ();

    private void Awake()
    {
        GameplayManager.instance.onStateChange += CheckState;
    }

    private void OnDestroy()
    {
        GameplayManager.instance.onStateChange -= CheckState;
    }

    void CheckState(GameplayState state)
    {
        switch (state)
        {
            case GameplayState.Day:

                foreach (var item in currentEnemyWave)
                {
                    item.GetComponent<Enemy>().onDied -= SpawnEnemyWhenDied;
                }

                currentEnemyWave.Clear();
                break;

            case GameplayState.Night:

                foreach (var item in wave[GameplayManager.instance.waveIndex].list)
                {
                    GameObject go = Instantiate(item, transform.position, Quaternion.identity, transform);
                    currentEnemyWave.Add(go);
                    go.GetComponent<Enemy>().onDied += SpawnEnemyWhenDied;
                    go.SetActive(false);
                }

                InitSpawnEnemy(wave[GameplayManager.instance.waveIndex].amountFirstSpawn);
                break;

        }
    }

    private void InitSpawnEnemy(int numberOfSpawn)
    {
        int rand = UnityEngine.Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < numberOfSpawn; i++)
        {
            currentEnemyWave[i].transform.position = spawnPoints[(rand + i) % spawnPoints.Length].position;
            currentEnemyWave[i].SetActive(true);
            //spawnTracker++;
        }
    }

    private void SpawnEnemyWhenDied()
    {
        spawnTracker++;
        
        if (spawnTracker == wave[GameplayManager.instance.waveIndex].list.Count)
        {
            if (GameplayManager.instance.waveIndex == wave.Length - 1)
                GameplayManager.instance.Win();
            else
                GameplayManager.instance.ChangeToDay();

            spawnTracker = 0;
            return;
        }

        currentEnemyWave[spawnTracker].transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        currentEnemyWave[spawnTracker].SetActive(true);
        
    }
}
