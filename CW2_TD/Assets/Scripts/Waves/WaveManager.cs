using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Setup")]
    public WaveData[] Waves;

    [Header("Spawner References")]
    public EnemySpawner[] Spawners;
    [SerializeField] private int CurrentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(RunWaves()); //Its easier to explain Courintes with this link https://docs.unity3d.com/6000.3/Documentation/Manual/Coroutines.html
        // ill still try my best, tldr, its a function statement with a "yield" inside of it, able to be suspended when we need to, every frame for example.
    }

    IEnumerator RunWaves()
    {
        while (CurrentWaveIndex < Waves.Length)
        {
            Debug.Log($"Starting Wave {CurrentWaveIndex + 1}"); //Just tells us what wave we are currently on.
            WaveData CurrentWave = Waves[CurrentWaveIndex];

            foreach (WaveSpawn Spawn in CurrentWave.Spawns)
            {
                EnemySpawner ChosenSpawner = Spawners[Random.Range(0, Spawners.Length)];
                yield return StartCoroutine(
                    ChosenSpawner.SpawnWave(Spawn.EnemyType, Spawn.Count, Spawn.SpawnDelay));
            }
            Debug.Log($"Finished spawning Wave {CurrentWaveIndex + 1}");

            CurrentWaveIndex++;
            yield return new WaitForSeconds(3f);
        }

        Debug.Log("All waves completed.");
    }

}
