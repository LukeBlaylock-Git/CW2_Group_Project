using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public int EnemiesAlive = 0;

    public void RegisterEnemy()
    {
        EnemiesAlive++;
        Debug.Log("Enemy Registered. Alive: " + EnemiesAlive);
    }

    public void UnRegisterEnemy()
    {
        EnemiesAlive--;
        Debug.Log("Enemy Removed. Alive: " + EnemiesAlive);

        if (EnemiesAlive <= 0)
        {
            Debug.Log("All enemies cleared");
        }
    }
}
