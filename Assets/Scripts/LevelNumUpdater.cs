using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Update level number in UI text
public class LevelNumUpdater : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI levelNumberText = GetComponent<TextMeshProUGUI>(); // Get the text component on this game object
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // Get this scene index num (starts with 0)
        int levelNumber = sceneIndex + 1; // Scene number to be written
        levelNumberText.text = $"#{levelNumber}"; // Assign the contents to the UI element
    }
}