using UnityEngine;

public class JumpController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Start jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }

        // End jump when grounded
        if (IsGrounded() && animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", false);
        }
    }

    // Replace this with your actual grounded check
    bool IsGrounded()
    {
        // Simple example: always returns true for now
        return true;
    }
}
