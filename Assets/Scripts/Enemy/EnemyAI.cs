using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Chase")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;

    [Header("Attack")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Separation")]
    [SerializeField] private float separationRadius = 1f;
    [SerializeField] private float separationForce = 3f;
    [SerializeField] private LayerMask enemyLayer;

    private Transform player;
    private Rigidbody2D rb;
    private float lastAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 separation = GetSeparationForce();

        if (distance <= attackRange)
        {
            rb.linearVelocity = separation * separationForce;
            TryAttack();
        }
        else if (distance <= detectionRange)
        {
            Vector2 chaseDirection = (player.position - transform.position).normalized;
            Vector2 finalDirection = chaseDirection + separation;
            rb.linearVelocity = finalDirection.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private Vector2 GetSeparationForce()
    {
        Vector2 pushAway = Vector2.zero;
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationRadius, enemyLayer);

        foreach (Collider2D col in nearbyEnemies)
        {
            if (col.gameObject == gameObject) continue;

            Vector2 diff = (Vector2)transform.position - (Vector2)col.transform.position;
            float dist = diff.magnitude;

            if (dist > 0)
            {
                pushAway += diff.normalized / dist;
            }
        }

        return pushAway;
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}