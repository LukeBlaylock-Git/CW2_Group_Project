using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector_UI : MonoBehaviour
{
    public string[] Levels;

    public void LoadLevel(int Index)
    {
        if (Index < 0 || Index >= Levels.Length)
        {
            Debug.LogWarning("Invalid level Index");
            return;
        }
        SceneManager.LoadScene(Levels[Index]);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
