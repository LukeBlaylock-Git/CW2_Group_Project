using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public SplineContainer Path; //Public for posterity, but do not touch, automatically assigned by EnemySpawner
    public EnemyData Data;
    public PlayerEconomy MoneyHandler;

    [Header("Runtime")]
    public float CurrentHealth;
    public float Progress = 0f;
    public int DeathReward;
    //private int ActiveMoney = 0;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = Data.MaxHealth;
        DeathReward = Data.Reward;
        
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
            MoneyHandler.AddMoney(DeathReward);
            Die();
        }
    }
    public void Die()
    {
        //Debug.Log($"{name} died.");

    
        Destroy(gameObject); //Will soon also give the player money upon "death" dependent on what unit it was.

    }

    public void ReachGoal()
    {
        Destroy(gameObject); //Later must be updated to reduce lives.
    }
}
