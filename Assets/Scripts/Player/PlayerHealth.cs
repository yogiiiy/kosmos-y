using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private PlayerHealthBar healthBar;
    private float currentHealth;
    private void UpdateHealthUI() // daripada ngulang if terus, bikin method buat update health UI
    {
        if (healthBar != null)
        {
        healthBar.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        
        UpdateHealthUI(); // kita pakai disini 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI(); // ini juga sama

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI(); // ini juga sama
    }

    private void Die()
    {
        Debug.Log("Player mati!");
        // nanti bisa ditambah restart scene / game over screen
    }
}