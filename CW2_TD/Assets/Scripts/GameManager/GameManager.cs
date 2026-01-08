using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    bool Paused = false;
    public static GameManager Instance;

    [Header("Meta Stats")]
    public int Lives = 10; //Number of lives our player starts with, can be configured freely, 10 is just the default.
    public int Money = 200; //Money that our player starts with, again can be configured freely,
    public int EnemiesAlive {  get; private set; } 

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

    void Update()
    {
        CursorMode();

        if (Input.GetKeyDown(KeyCode.Escape)) //if Escape is pressed, pause game.
        {
            TogglePause();
        }
    }
    void CursorMode()
    {
        switch (CurrentPhase)
        {
            //Below is making sure our mouse is controllable and visible during certain phases of the game.
            case GamePhase.Build:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            //Cursor is locked and hidden during combat.
            case GamePhase.Combat:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            //Cursor is free when the game ends.
            case GamePhase.End:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
    //Called when the player loses a life, so when the enemy reaches the end of our spline.
    public void LifeLost(int Amount = 1)
    {
        Lives -= Amount;

        if (Lives <= 0) //If lives are zero, trigger function GameOver
        {
            GameOver();
        }
    }
    public void GameOver() //Handles our lose condition.
    {
        Debug.Log("Lives reached 0, Game Over");

        CurrentPhase = GamePhase.End;
        Time.timeScale = 0f;

        StartCoroutine(ReturnToMenuAfterDelay());
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
    void TogglePause() //Function for toggling game pausing with esc
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
        Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = Paused;
    }
    public void RegisterEnemy()
    {
        EnemiesAlive++;
    }
    public void UnRegisterEnemy()
    {
        EnemiesAlive--;

        if (EnemiesAlive < 0)
            EnemiesAlive = 0;
    }
    IEnumerator ReturnToMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f; // Unpauses the game, without this it loads the scene but you cant interact or do anything.
        SceneManager.LoadScene("MainMenu");
    }
    public void GameWon() //Handles the "winning" of the game, calls the gamephase End and returns the player to the main menu after a few seconds.
    {
        Debug.Log("All waves completed, game Won");
        CurrentPhase = GamePhase.End;
        Time.timeScale = 0f;

        StartCoroutine(ReturnToMenuAfterDelay());
    }
        
}
