using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject aboutPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void AboutGame()
    {
        aboutPanel.SetActive(true);
    }
    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }
}