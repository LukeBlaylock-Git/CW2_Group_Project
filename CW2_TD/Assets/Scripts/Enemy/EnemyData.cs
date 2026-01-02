using UnityEngine;

[CreateAssetMenu(menuName = "TD/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Prefab")]
    public Enemy EnemyPrefab;

    [Header("Stats")]
    public float MaxHealth = 10f;
    public float MoveSpeed = 0.25f;
    public int Reward = 2;
}
