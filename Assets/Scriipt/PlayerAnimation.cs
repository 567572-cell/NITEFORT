using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()

{
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    float speed = new Vector2(x, z).magnitude;
    anim.SetFloat("Speed", speed);
}


} 
