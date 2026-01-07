using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public enum BulletDirection
    {
        CameraHorizontal,
        CameraFull,
        PlayerForward,
        FirePointForward
    }

    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public float bulletSpeed = 25f;
    public float damage = 25f;
    [SerializeField] public BulletDirection bulletDirection = BulletDirection.CameraHorizontal;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Camera cam = Camera.main;
        Vector3 aimDirection = Vector3.forward;

        switch (bulletDirection)
        {
            case BulletDirection.CameraHorizontal:
                aimDirection = cam.transform.forward;
                aimDirection.y = 0;
                aimDirection = aimDirection.normalized;
                break;
            case BulletDirection.CameraFull:
                aimDirection = cam.transform.forward;
                break;
            case BulletDirection.PlayerForward:
                aimDirection = transform.forward;
                break;
            case BulletDirection.FirePointForward:
                aimDirection = firePoint.forward;
                break;
        }

        Vector3 targetPoint;
        RaycastHit hit;
        Ray ray = new Ray(firePoint.position, aimDirection);

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            targetPoint = hit.point;

            // ✅ Damage enemy if hit
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            targetPoint = firePoint.position + aimDirection * 1000f;
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed; // ✅ fixed (no linearVelocity)
    }
}
