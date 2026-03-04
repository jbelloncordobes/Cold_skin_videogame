using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        if (!playerHealth || !fillImage) return;
        fillImage.fillAmount = playerHealth.Normalized;
    }
}