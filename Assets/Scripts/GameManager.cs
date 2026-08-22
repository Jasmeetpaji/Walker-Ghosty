using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    public void GameOver()
    {
        gameOver = true;

        Debug.Log("GAME OVER!");

        Time.timeScale = 0f;
    }
}
