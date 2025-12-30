using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;         // enemy prefab
    public EnemyData EnemyType;            // enemy stats
    public SplineContainer SplinePath;     // spline IN THE SCENE

    public float SpawnInterval = 2f;
    private float Timer = 0f;

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= SpawnInterval)
        {
            Timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Spawn one enemy
        GameObject e = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

        // Get the movement component
        EnemySplineMovement move = e.GetComponent<EnemySplineMovement>();

        // Assign data + spline
        move.Type = EnemyType;
        move.Path = SplinePath;

        Debug.Log("Enemy Spawned");
    }
}
