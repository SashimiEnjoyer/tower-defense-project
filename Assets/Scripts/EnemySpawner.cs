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
    public Wave[] wave;
    [SerializeField] Transform[] spawnPoints;

    int currentWave = 0;
    int spawnTracker = 0;
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
                    item.GetComponent<Enemy>().onDied -= () => SpawnEnemy(1);
                }

                currentEnemyWave.Clear();
                break;

            case GameplayState.Night:

                foreach (var item in wave[currentWave].list)
                {
                    GameObject go = Instantiate(item, transform.position, Quaternion.identity, transform);
                    currentEnemyWave.Add(go);
                    go.GetComponent<Enemy>().onDied += () => SpawnEnemy(1);
                    go.SetActive(false);
                }

                SpawnEnemy(wave[currentWave].amountFirstSpawn);
                break;

        }
    }

    public void SpawnEnemy(int numberOfSpawn)
    {
        for (int i = 0; i < numberOfSpawn; i++)
        {

            if (spawnTracker == (wave[currentWave].list.Count)) 
            { 
                GameplayManager.instance.ChangeState(GameplayState.Day);
                break;
            }

            currentEnemyWave[spawnTracker].transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
            currentEnemyWave[spawnTracker].SetActive(true);
            spawnTracker++;
        }
    }

}
