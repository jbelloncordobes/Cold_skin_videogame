using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private MonoBehaviour playerLookScript; // your mouse look script

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += ShowGameOver;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        // Show UI
        gameOverPanel.SetActive(true);

        // Stop time
        Time.timeScale = 0f;

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable mouse look if needed
        if (playerLookScript != null)
            playerLookScript.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}