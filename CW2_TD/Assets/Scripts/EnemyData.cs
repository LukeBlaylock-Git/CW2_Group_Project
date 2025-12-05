using UnityEngine;

[CreateAssetMenu(menuName = "TD/Enemy Type")]
public class EnemyData : ScriptableObject
{
    public string EnemyName = "Basic Enemy";
    public float MaxHealth = 10; //How many hits before the enemy dies
    public float MoveSpeed = 3; //How fast the enemy moves through the track
    public float TurnSpeed = 5; //how fast it turns
    public int reward = 1; //coins to be awarded upon killing

    public GameObject EnemyPrefab;
}
