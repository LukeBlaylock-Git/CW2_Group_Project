using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public EnemyData Stats ; //Call in the enemy data
    public float CurrentHealth = 10;
    void Start()
    {
        float CurrentHealth = Stats.MaxHealth; //Assign the needed data
    }

    public void DamageTaken(int amount)
    {
        CurrentHealth -= amount; // How much damage

        if (CurrentHealth <= 0)// Are they dead
        {
            Destroy(gameObject);//Destroy if dead
        }
    }
}
