using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public float speed = 0f;
    public  Actions actions;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (speed > 3)
        {
            actions.Run();
        }

        if (speed < 3)
        {
            actions.Stay();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Jump()
    {
    animator.SetTrigger("Jump");
    }

    public void SetAiming(bool value)
    {
        animator.SetBool("Aiming", value);
    }
}
