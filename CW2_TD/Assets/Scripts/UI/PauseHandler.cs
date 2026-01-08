using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject PauseMenu;

    public void Pause()
    { 
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
