using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; 
    private bool isPaused = false; 

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); 
            Time.timeScale = 1f;        
            isPaused = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f;       
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f;        
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        Debug.Log("Выход в Главное Меню...");
        
        SceneManager.LoadScene("MainMenu"); 
    }
}