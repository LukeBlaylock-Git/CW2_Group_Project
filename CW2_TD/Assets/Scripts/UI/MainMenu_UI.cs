using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{
    public string[] Levels;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); //Load the game scene with the appropriate text
    }

    public void QuitGame() //if button clicked, quit game.
    {
        Application.Quit();
    }
}
