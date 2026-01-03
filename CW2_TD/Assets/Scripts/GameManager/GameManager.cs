using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Meta Stats")]
    public int Lives = 10;
    public int Money = 200;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); //Making sure theres no duplicate GMs
    }
    public enum GamePhase
    {
        Build,
        Combat,
        End
    }
    public GamePhase CurrentPhase = GamePhase.Build; //Starts us off on the build phase.
    public void LifeLost(int Amount = 1)
    {
        Lives -= Amount;

        if (Lives <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        Debug.Log("Lives reached 0, Game Over");
        Time.timeScale = 0f; //Pause on game over.
    }
    public bool CanAfford(int Cost)
    {
        return Money >= Cost;
    }
    public void SpendMoney(int Cost)
    {
        Money -= Cost;
    }
    public void AddMoney(int Amount)
    {
        Money += Amount;
    }

}
