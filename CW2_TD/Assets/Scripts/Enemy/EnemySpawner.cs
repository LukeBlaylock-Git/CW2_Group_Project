using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("Path")]
    public SplineContainer Path;
    public PlayerEconomy PlayerEconomy;


    public IEnumerator SpawnWave(EnemyData EnemyData, int Count, float Delay)
    {
        for (int i = 0; i < Count; i++)
        {
            Enemy Enemy = Instantiate(EnemyData.EnemyPrefab, Vector3.zero, Quaternion.identity);
            Enemy.Path = Path;
            Enemy.Data = EnemyData;
            Enemy.MoneyHandler = PlayerEconomy;
            yield return new WaitForSeconds(Delay);
        }
    }
   
}