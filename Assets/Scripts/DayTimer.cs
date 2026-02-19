using UnityEngine;
using UnityEngine.SceneManagement;

public class DayTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDuration = 300f;
    public float nightDuration = 300f;

    [Header("Current State")]
    public bool isDay = true;
    public float timeRemaining;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        timeRemaining = dayDuration;

        LoadUIScene();
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            if (isDay)
                TransitionToNight();
            else
                TransitionToDay();
        }
    }

    void TransitionToNight()
    {
        Debug.Log("Night is coming...");
        isDay = false;
        timeRemaining = nightDuration;
        SceneManager.LoadScene("NightScene", LoadSceneMode.Single);
        LoadUIScene();
    }

    void TransitionToDay()
    {
        Debug.Log("Day has come...");
        isDay = true;
        timeRemaining = dayDuration;
        SceneManager.LoadScene("DayScene", LoadSceneMode.Single);
        LoadUIScene();
    }

    void LoadUIScene()
    {
        // Only load if not already loaded
        if (!SceneManager.GetSceneByName("UIScene").isLoaded)
        {
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }
    }

    public float GetTimeNormalized()
    {
        float total = isDay ? dayDuration : nightDuration;
        return timeRemaining / total;
    }

    public string GetTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}