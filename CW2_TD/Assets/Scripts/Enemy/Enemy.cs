using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    [Header("References")] //References to other game objects, data, etc.
    public SplineContainer Path; //Public for posterity, but do not touch, automatically assigned by EnemySpawner
    public EnemyData Data;

    [Header("Runtime")]
    public float CurrentHealth;
    public float Progress = 0f; //Where our enemy is on the spline, 0 is the start, 1 is the end, do not touch.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = Data.MaxHealth; //Make the current health the same as the MaxHealth in Data
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath(); //Call function move along path
    }
    public void MoveAlongPath() //Function to handle movement along a spline.
    {
        if (Path == null || Data == null) return; //Just a edge case check to see if the required data is missing, should never occur, but safe then sorry.

        Progress += Data.MoveSpeed * Time.deltaTime; //Move the enemy along the path based on its move speed
        Progress = Mathf.Clamp01(Progress); //Making sure the Progress stays between 0 and 1

        transform.position = Path.EvaluatePosition(Progress); //Update the enemy's position in the game using the assigned spline.

        if (Progress >= 1f) //If the enemy reaches the end of the path, trigger "ReachGoal" function
        {
            ReachGoal();
        }
    }
    public void TakeDamage(float Damage) //Called when the enemy takes damage from a tower projectile or the player.
    {
        CurrentHealth -= Damage; 

        Debug.Log($"{name} took {Damage} damage. HP now: {CurrentHealth}"); 

        if (CurrentHealth <= 0f) //Health reaches 0, call function for "die" is triggered.
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
        GameManager.Instance.UnRegisterEnemy();
        Destroy(gameObject); //Will soon also give the player money upon "death" dependent on what unit it was.
    }

    public void ReachGoal()
    {
        GameManager.Instance.LifeLost(Data.Damage); //Reduces lives
        GameManager.Instance.UnRegisterEnemy(); //Notify the game manager that the enemy has been removed, used for wave completion logic.
        Destroy(gameObject);  //Removes the game object, garbage cleanup.
    }
}
