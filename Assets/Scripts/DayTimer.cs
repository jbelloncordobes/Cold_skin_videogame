using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DayTimer : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDuration = 30f;
    public float nightDuration = 30f;

    [Header("Current State")]
    public bool isDay = true;
    public float timeRemaining;

    private bool isTransitioning = false;

    void Awake()
    {
        // Use Awake instead of Start so it persists before anything else runs
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        timeRemaining = dayDuration;
        StartCoroutine(LoadUISceneAsync());
    }

    void Update()
    {
        if (isTransitioning) return;

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
        isTransitioning = true;
        isDay = false;
        timeRemaining = nightDuration;
        StartCoroutine(LoadSceneAndUI("NightScene"));
    }

    void TransitionToDay()
    {
        isTransitioning = true;
        isDay = true;
        timeRemaining = dayDuration;
        StartCoroutine(LoadSceneAndUI("3D_island_demo"));
    }

    IEnumerator LoadSceneAndUI(string sceneName)
    {
        Debug.Log("Starting transition to: " + sceneName);

        // Step 1 - Unload UIScene first cleanly
        if (SceneManager.GetSceneByName("UIScene").isLoaded)
        {
            AsyncOperation unloadUI = SceneManager.UnloadSceneAsync("UIScene");
            while (!unloadUI.isDone)
                yield return null;
            Debug.Log("UIScene unloaded");
        }

        // Step 2 - Load the gameplay scene
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(
            sceneName, LoadSceneMode.Single);
        loadScene.allowSceneActivation = true;
        while (!loadScene.isDone)
            yield return null;
        Debug.Log("GameScene loaded: " + sceneName);

        // Step 3 - Wait one frame to let scene initialize
        yield return null;

        // Step 4 - Reload UIScene on top
        AsyncOperation loadUI = SceneManager.LoadSceneAsync(
            "UIScene", LoadSceneMode.Additive);
        while (!loadUI.isDone)
            yield return null;
        Debug.Log("UIScene reloaded");

        isTransitioning = false;
    }

    IEnumerator LoadUISceneAsync()
    {
        if (!SceneManager.GetSceneByName("UIScene").isLoaded)
        {
            AsyncOperation loadUI = SceneManager.LoadSceneAsync(
                "UIScene", LoadSceneMode.Additive);
            while (!loadUI.isDone)
                yield return null;
            Debug.Log("UIScene loaded for first time");
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