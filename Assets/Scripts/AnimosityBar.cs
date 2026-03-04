using UnityEngine;
using UnityEngine.UI;

public class AnimosityBar : MonoBehaviour
{
	public GameObject animosityBarUI;
	public Image fill;
	public float hideDelay = 3f;

	float _value = 0f;
	float _timer = 0f;
	float _testTimer = 2f;

	public static AnimosityBar Instance { get; private set; }

	void Awake()
	{
		Instance = this;
		animosityBarUI.SetActive(false);
	}

	void Update()
	{
		_testTimer -= Time.deltaTime;
		if (_testTimer <= 0f)
		{
			_testTimer = 2f;
			_value += 0.1f;       // increase by 10% each time
			if (_value > 1f) _value = 0f; // reset to 0 when full
			SetAnimosity(_value);
		}

		if (animosityBarUI.activeSelf)
		{
			_timer -= Time.deltaTime;
			if (_timer <= 0f)
				animosityBarUI.SetActive(false);
		}
	}

	public void SetAnimosity(float value)
	{
		_value = Mathf.Clamp01(value);
		fill.fillAmount = _value;
		animosityBarUI.SetActive(true);
		_timer = hideDelay;
	}
}