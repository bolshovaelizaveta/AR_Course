using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Панели UI")]
    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;

    [Header("Тексты")]
    public TextMeshProUGUI scoreText;      
    public TextMeshProUGUI finalScoreText; 
    public TextMeshProUGUI bestScoreText;  

    private int score = 0;
    private int highScore = 0;
    private bool isPaused = false;
    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddPoint()
    {
        score++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Fish: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; 

        // Рекорд: сравниваем текущий счет с тем, что в памяти
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        
        if (score > savedHighScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); 
        }
        else
        {
            highScore = savedHighScore; 
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "YOUR SCORE: " + score;
            bestScoreText.text = "BEST RECORD: " + highScore;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}