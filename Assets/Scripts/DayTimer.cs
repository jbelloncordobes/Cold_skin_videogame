using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DayTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDuration = 30f;

    [Header("Current State")]
    public float timeRemaining;

    private bool isTransitioning = false;

    void Start()
    {
        timeRemaining = dayDuration;
    }

    void Update()
    {
        if (isTransitioning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            isTransitioning = true;
            StartCoroutine(LoadNightScene("NightScene"));
        }
    }

    IEnumerator LoadNightScene(string sceneName)
    {
        Debug.Log("Starting transition to: " + sceneName);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public float GetTimeNormalized()
    {
        return timeRemaining / dayDuration;
    }

    // For access outsie the script
    public string GetTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}