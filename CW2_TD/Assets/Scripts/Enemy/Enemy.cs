using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public SplineContainer Path; //Public for posterity, but do not touch, automatically assigned by EnemySpawner
    public EnemyData Data;

    [Header("Runtime")]
    public float CurrentHealth;
    public float Progress = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = Data.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }
    public void MoveAlongPath()
    {
        if (Path == null || Data == null) return;

        Progress += Data.MoveSpeed * Time.deltaTime;
        Progress = Mathf.Clamp01(Progress);

        transform.position = Path.EvaluatePosition(Progress);

        if (Progress >= 1f)
        {
            ReachGoal();
        }
    }
    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;

        Debug.Log($"{name} took {Damage} damage. HP now: {CurrentHealth}");

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        if ( (Data != null && GameManager.Instance != null))
        {
            GameManager.Instance.AddMoney(Data.Reward);
        }
        {
            
        }
        Debug.Log($"{name} died.");
        Destroy(gameObject); //Will soon also give the player money upon "death" dependent on what unit it was.
    }

    public void ReachGoal()
    {
        GameManager.Instance.LifeLost(Data.Damage); //Reduces lives
        Destroy(gameObject); 
    }
}
