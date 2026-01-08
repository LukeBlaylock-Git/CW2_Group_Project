using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuButton : MonoBehaviour
{

    public void LoadMainMenu()
    {
        Debug.Log("Works");
        SceneManager.LoadScene("MainMenu");
        // return;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
