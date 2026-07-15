using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private GameObject itemPickupPrefab;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (itemPickupPrefab != null)
        {
            Instantiate(itemPickupPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}