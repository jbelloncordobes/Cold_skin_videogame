using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
	public Image healthBar;
	public Image hungerBar;
	public Image thirstBar;
	public Image energyBar;
	public Image warmthBar;
	public Image stressBar;

	public Image playerHealthFill;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		stressBar.fillAmount = 0f;
	}

	void Update()
	{
		healthBar.fillAmount -= 0.10f * Time.deltaTime;
		hungerBar.fillAmount -= 0.10f * Time.deltaTime;
		thirstBar.fillAmount -= 0.10f * Time.deltaTime;
		energyBar.fillAmount -= 0.10f * Time.deltaTime;
		warmthBar.fillAmount -= 0.10f * Time.deltaTime;
		stressBar.fillAmount += 0.10f * Time.deltaTime;

		playerHealthFill.fillAmount = healthBar.fillAmount;
	}
}