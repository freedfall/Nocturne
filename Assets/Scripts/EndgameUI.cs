using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}