using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver = false;
    public int coins = 0;
    public int score = 0;
    [Header("Speed Settings")]
    public float startingSpeed = 5f;
    public float speedIncrease = 1f;
    public int scoreInterval = 250;
    public GameObject gameOverPanel;
    [Header("Sound")]
    public AudioSource gameOverAudioSource;
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
            player.moveSpeed = startingSpeed;
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
            int speedLevel = score / scoreInterval;
            player.moveSpeed = startingSpeed +
                               (speedLevel * speedIncrease);
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
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (gameOverAudioSource != null)
        {
            gameOverAudioSource.Play();
        }
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
