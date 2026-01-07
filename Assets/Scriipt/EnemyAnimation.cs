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
        //animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        speed = agent.velocity.magnitude;
        if (speed > 3)
        {
            actions.Run();
        }

        if (speed < 3)
        {
            actions.Stay();
        }
    }
}
