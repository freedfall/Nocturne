using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Canvas tutorialCanvas;
    public Canvas startCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenTutorial()
    {
        tutorialCanvas.gameObject.SetActive(true);
        startCanvas.gameObject.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialCanvas.gameObject.SetActive(false);
        startCanvas.gameObject.SetActive(true);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}