using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public Button StartWaveButton;

    [Header("Wave Setup")]
    public WaveData[] Waves;
    public bool ActiveWave = false; //Do not touch, its for the wave logic.

    [Header("Spawner References")]
    public EnemySpawner[] Spawners;
    [SerializeField] private int CurrentWaveIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
   /* void Start()
    {
        StartCoroutine(RunWaves()); //Its easier to explain Courintes with this link https://docs.unity3d.com/6000.3/Documentation/Manual/Coroutines.html
        // ill still try my best, tldr, its a function statement with a "yield" inside of it, able to be suspended when we need to, every frame for example.
    } */

    IEnumerator RunWaves()
    {
        if (CurrentWaveIndex >= Waves.Length)
        {
            Debug.Log("All waves complete");
            yield break;
        }

        Debug.Log($"Starting wave {CurrentWaveIndex + 1}");
        WaveData CurrentWave = Waves[CurrentWaveIndex];

        foreach (WaveSpawn Spawn in CurrentWave.Spawns)
        {
            EnemySpawner ChosenSpawner = Spawners[Random.Range(0, Spawners.Length)];
            yield return StartCoroutine(ChosenSpawner.SpawnWave(Spawn.EnemyType, Spawn.Count, Spawn.SpawnDelay));
        }
        Debug.Log($"Finished Spawning Wave {CurrentWaveIndex + 1}");

        yield return new WaitUntil(() => GameManager.Instance.EnemiesAlive == 0); //Waiting until all enemies are dead.

        ActiveWave = false; //When we ave is ended we run the below lines.
        StartWaveButton.interactable = true;
        GameManager.Instance.CurrentPhase = GameManager.GamePhase.Build;

        CurrentWaveIndex++; // Move to next wave.

        if (CurrentWaveIndex >= Waves.Length) //If that was the last wave, move to the ending phase.
        {
            Debug.Log("All waves completed");
            GameManager.Instance.CurrentPhase = GameManager.GamePhase.End;
            StartWaveButton.gameObject.SetActive(false);
        }
    }
    public int CurrentWave //Serves one purpose and one purpose only, telling the UI what wave we are on.
    {
        get { return CurrentWaveIndex; }
    }

    public void StartWaves()
    {
        if (ActiveWave) return;
        ActiveWave = true;
        StartWaveButton.interactable = false;

        GameManager.Instance.CurrentPhase = GameManager.GamePhase.Combat;

        StartCoroutine(RunWaves());
    }
}
