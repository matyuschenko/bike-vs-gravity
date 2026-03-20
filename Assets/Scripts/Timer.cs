using UnityEngine;
using TMPro;

// Start and manage timer and assign the time to a UI text
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText; // UI text to show the time
    public Color TimerStopColor; // When timer stopped it will change to this color

    private float _elapsedTime; // Time since the timer started
    private bool _isTimerRunning = true;

    void Update()
    {
        if (_isTimerRunning)
        {
            // Accumulate time every frame, independent of frame rate
            _elapsedTime += Time.deltaTime;
            DisplayTime(_elapsedTime);
        }
    }

    // Show the time in the UI element
    void DisplayTime(float timeToDisplay)
    {
        // Ensure the time doesn't show negative values if stopped at zero
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        // Calculate minutes, seconds, deciseconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int deciseconds = Mathf.FloorToInt(timeToDisplay % 1 * 10);

        // Format the time string to "00:00.0"
        // The format specifier ensures leading zeros for single-digit numbers
        string timeString = string.Format("{0:00}:{1:00}.{2}", minutes, seconds, deciseconds);

        // Update the UI text
        if (TimerText != null)
        {
            TimerText.text = timeString;
        }
    }

    // Methods to control the timer
    public void StartTimer()
    {
        _isTimerRunning = true;
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
        TimerText.color = TimerStopColor; // Color the UI text to a specific color when timer is stopped
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        DisplayTime(_elapsedTime);
    }
}
