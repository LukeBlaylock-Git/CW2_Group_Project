using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("Path")]
    public SplineContainer Path;

    public IEnumerator SpawnWave(EnemyData EnemyData, int Count, float Delay)
    {
        for (int i = 0; i < Count; i++)
        {
            Enemy Enemy = Instantiate(EnemyData.EnemyPrefab, Vector3.zero, Quaternion.identity);
            Enemy.Path = Path;
            Enemy.Data = EnemyData;
            GameManager.Instance.RegisterEnemy();

            yield return new WaitForSeconds(Delay);
        }
    }
}