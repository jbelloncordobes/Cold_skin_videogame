using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimosityBar : MonoBehaviour
{
    public GameObject animosityBarUI;
    public Image fill;
    public float hideDelay = 3f;
	public float value;
	public TMP_Text animosityText;
	public TMP_Text multiplierText;

    float _timer = 0f;

    public static AnimosityBar Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        animosityBarUI.SetActive(false);
    }

    void Update()
    {
        if (!animosityBarUI.activeSelf) return;

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
            animosityBarUI.SetActive(false);
    }

    public void SetAnimosity(float newValue)
	{
		value = Mathf.Clamp01(newValue);
		fill.fillAmount = value;

		GameManager.Instance.animosity = value; // In the demo there are enough enemies during a single night to observe the effects of animosity from 0 to 100
		animosityText.text = $"{Mathf.RoundToInt(value * 100)}%";

		animosityBarUI.SetActive(true);
		_timer = hideDelay;

		float multiplier = Mathf.Lerp(0.5f, 1.5f, value);
		multiplierText.text = $"{Mathf.RoundToInt(multiplier * 100)}%";

		Debug.Log($"New Multiplier: {multiplier}");

		EnemyHealth[] enemies_health = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

		foreach (EnemyHealth enemy in enemies_health)
		{
			enemy.SetMultiplier(multiplier);
		}

		EnemyAStarAI[] enemies_AI = FindObjectsByType<EnemyAStarAI>(FindObjectsSortMode.None);

		foreach (EnemyAStarAI enemy in enemies_AI)
		{
			enemy.SetMultiplier(multiplier);
		}

		Debug.Log($"New animosity level: {value}");
	}
}