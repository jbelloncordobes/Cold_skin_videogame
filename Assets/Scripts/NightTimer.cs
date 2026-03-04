using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NightTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float nightDuration = 30f;

    [Header("Current State")]
    public float timeRemaining;

    private bool isTransitioning = false;

    void Start()
    {
        timeRemaining = nightDuration;
    }

    void Update()
    {
        if (isTransitioning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            isTransitioning = true;
            StartCoroutine(LoadDayScene("3D_island_demo"));
        }
    }

    IEnumerator LoadDayScene(string sceneName)
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
        return timeRemaining / nightDuration;
    }

    // For access outsie the script
    public string GetTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}