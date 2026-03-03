using UnityEngine;
using TMPro;

public class NightTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    // public TextMeshProUGUI dayNightText;

    private NightTimer nightTimer;

    void Start()
    {
        nightTimer = FindFirstObjectByType<NightTimer>();
        Debug.Log("NightTimerUI Start - NightTimer found: " + (nightTimer != null));
        Debug.Log("TimerText assigned: " + (timerText != null));
    }

    void Update()
    {
        if (nightTimer == null)
        {
            nightTimer = FindFirstObjectByType<NightTimer>();
            Debug.Log("Searching for NightTimer...");
            return;
        }

        timerText.text = nightTimer.GetTimeFormatted();
        // dayNightText.text = dayTimer.isDay ? "Day" : "Night";
        // dayNightText.text = "Day";

        if (nightTimer.timeRemaining <= 60f)
            timerText.color = Color.Lerp(Color.red, Color.white,
                Mathf.PingPong(Time.time * 2f, 1f));
        else
            timerText.color = Color.white;
    }
}