using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver = false;
    public int coins = 0;
    public int score = 0;
    public GameObject gameOverPanel;
    private float startX;
    private PlayerMovement player;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            startX = player.transform.position.x;
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    void Update()
    {
        if (!gameOver && player != null)
        {
            float distance = player.transform.position.x - startX;
            score = Mathf.Max(0, Mathf.FloorToInt(distance));
        }
    }
    public void AddCoin()
    {
        coins++;
    }
    public void GameOver()
    {
        if (gameOver)
            return;
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();

        Debug.Log("Quit Game");
    }
}