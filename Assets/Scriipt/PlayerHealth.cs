using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    bool isDead = false;

    Animator animator;
    CharacterController controller;
    public GameObject gun;


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

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

        Debug.Log("Player died – playing animation");

        // ✅ PLAY ANIMATION FIRST
        animator.SetTrigger("Die");

        // ✅ STOP MOVEMENT ONLY
        controller.enabled = false;

        // ✅ DISABLE GUN (NOT PLAYER)
        if (gun != null)
            gun.SetActive(false);

        // ⏱ wait for animation to finish
        Invoke(nameof(Respawn), 3f);
    }



    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
