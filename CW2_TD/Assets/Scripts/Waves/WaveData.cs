using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveSpawn
{
    public EnemyData EnemyType;
    public int Count = 5;
    public float SpawnDelay = 1f;
}

[CreateAssetMenu(menuName = "TD/Wave Data")]
public class WaveData : ScriptableObject
{
    public List<WaveSpawn> Spawns = new List<WaveSpawn>();
}