using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    int currentHealth;
    bool isDead = false;

    Animator animator;          // Enemy Animator
    EnemyAttack enemyAttack;    // Attack script reference

    void Start()
    {
        currentHealth = maxHealth;

        // Grab Animator on this enemy (or child)
        animator = GetComponentInChildren<Animator>();

        // Grab attack script to disable on death
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Call this from bullets or player gun
    public void TakeDamage(int damage)
    {
        if (isDead) return;   // Already dead → ignore damage

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Enemy died");

        // 1️⃣ Play death animation
        animator.SetTrigger("Die");

        // 2️⃣ Stop enemy attacks
        if (enemyAttack != null)
            enemyAttack.enabled = false;

        // 3️⃣ Optional: stop enemy movement here if needed

        // 4️⃣ Respawn or reset after animation duration
        Invoke(nameof(Respawn), 3f);  // Adjust 3f to match your animation length
    }

    void Respawn()
    {
        // Reset health
        currentHealth = maxHealth;
        isDead = false;

        // Reset animator to Idle state
        animator.Rebind();
        animator.Update(0f);

        // Re-enable enemy attacks
        if (enemyAttack != null)
            enemyAttack.enabled = true;

        Debug.Log("Enemy respawned");
    }
}
