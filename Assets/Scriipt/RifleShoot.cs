using UnityEngine;

public class RifleShoot : MonoBehaviour
{
    public Camera playerCamera;
    public Transform muzzle;

    public float range = 100f;
    public float fireRate = 0.1f;
    public int damage = 10;

    float nextFireTime;

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
{
    anim.SetTrigger("Shoot");

    Ray ray = new Ray(playerCamera.transform.position,
                      playerCamera.transform.forward);

    if (Physics.Raycast(ray, out RaycastHit hit, range))
    {
        Debug.Log("Hit: " + hit.collider.name);
    }
}

}
