using UnityEngine;
using TMPro;

public class DayTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    // public TextMeshProUGUI dayNightText;

    private DayTimer dayTimer;

    void Start()
    {
        dayTimer = FindFirstObjectByType<DayTimer>();
        Debug.Log("DayTimerUI Start - DayTimer found: " + (dayTimer != null));
        Debug.Log("TimerText assigned: " + (timerText != null));
        // Debug.Log("DayNightText assigned: " + (dayNightText != null));
    }

    void Update()
    {
        if (dayTimer == null)
        {
            dayTimer = FindFirstObjectByType<DayTimer>();
            Debug.Log("Searching for DayTimer...");
            return;
        }

        timerText.text = dayTimer.GetTimeFormatted();
        // dayNightText.text = dayTimer.isDay ? "Day" : "Night";
        // dayNightText.text = "Day";

        if (dayTimer.timeRemaining <= 60f)
            timerText.color = Color.Lerp(Color.red, Color.white,
                Mathf.PingPong(Time.time * 2f, 1f));
        else
            timerText.color = Color.white;
    }
}