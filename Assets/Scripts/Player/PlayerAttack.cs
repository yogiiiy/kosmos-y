using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDistance = 0.5f; // jarak AttackPoint dari Player
    [SerializeField] private LayerMask enemyLayer;

    private PlayerControls controls;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        controls = new PlayerControls();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        controls.Player.Attack.performed -= OnAttack;
        controls.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Vector2 attackPosition = (Vector2)transform.position + playerMovement.LastMoveDirection * attackDistance;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
{
    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
    if (enemyHealth != null)
    {
        enemyHealth.TakeDamage(attackDamage);
    }
}
    }

    private void OnDrawGizmosSelected()
    {
        if (playerMovement == null) return;
        Vector2 attackPosition = (Vector2)transform.position + playerMovement.LastMoveDirection * attackDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
} 