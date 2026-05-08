using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health playerHealth;
    private Image fillImage;

    void Start()
    {
        fillImage = GetComponent<Image>();
    }

    void Update()
    {
        if (playerHealth == null) return;
        if (fillImage == null) return;
        
        fillImage.fillAmount = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        Debug.Log("Health: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth);
    }
}