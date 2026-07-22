using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private GameObject itemPickupPrefab;
    private float currentHealth;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (flashCoroutine != null)
        {
            
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRed());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed() // Buat Flash ketika kena DMG
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        flashCoroutine = null;
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