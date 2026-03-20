using UnityEngine;
using UnityEngine.SceneManagement;

// Restart the scene on trigger
public class RestartManager : MonoBehaviour
{
    // This function will be called when the button is pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the currently active scene
    }
}