using UnityEngine;

public class EnemyLifeCycle : MonoBehaviour
{
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null)
            gameManager.RegisterEnemy(); //Telling GameManager this enemy is now real.
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (gameManager != null)
            gameManager.UnRegisterEnemy(); //Opposite to register enemy, telling the game manager it now no longer registers.
    }
}
