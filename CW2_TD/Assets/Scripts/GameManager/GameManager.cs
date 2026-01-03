using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool Paused = false;
    public static GameManager Instance;

    [Header("Meta Stats")]
    public int Lives = 10;
    public int Money = 200;
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
            case GamePhase.Combat:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GamePhase.End:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
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
}
