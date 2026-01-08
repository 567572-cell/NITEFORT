using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    float nextAttackTime;

    Transform player;
    PlayerHealth playerHealth;
    Animator animator;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.transform;
        playerHealth = p.GetComponent<PlayerHealth>();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        // Damage applied immediately (we'll improve this next)
        playerHealth.TakeDamage(damage);
    }
}
