using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float health = 100f;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float damage = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distance <= attackRange)
            {
                Attack();
            }
        }
        animator.SetTrigger("Fire");

    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Example: damage the player
            player.GetComponent<PlayerHealth>()?.TakeDamage((int)Math.Round(damage));
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
