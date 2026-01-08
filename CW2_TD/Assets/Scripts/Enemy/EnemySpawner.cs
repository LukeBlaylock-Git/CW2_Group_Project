using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("Path")]
    public SplineContainer Path; //This is the path that will be assigned to our enemies that spawn at this spawner.

    public IEnumerator SpawnWave(EnemyData EnemyData, int Count, float Delay)
    {
        for (int i = 0; i < Count; i++) // Loop for the number of enemies that are in the wave data.
        {
            Enemy Enemy = Instantiate(EnemyData.EnemyPrefab, Vector3.zero, Quaternion.identity); //Spawning in said enemy.
            Enemy.Path = Path; //Assigning the path for the enemy,
            Enemy.Data = EnemyData; //Pulling the relevant data so the Enemy knows its stats.
            GameManager.Instance.RegisterEnemy(); //Inform the GameManager this enemy is now active in the scene.

            yield return new WaitForSeconds(Delay); //Wait based on the delay to spawn the next enemy.
        }
    }
}