using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootOrigin; // chest/head/etc
    [SerializeField] private Camera playerCamera;   // optional for aiming
    [SerializeField] private BulletTracer tracerPrefab;



    [Header("Shooting")]
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float fireRate = 10f; // shots per second
    [SerializeField] private LayerMask hitMask = ~0; // everything by default

    private float nextFireTime;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1f / fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
        // Direction:
        // If you want to shoot straight where the camera looks:
        Vector3 origin = playerCamera ? playerCamera.transform.position : shootOrigin.position;
        Vector3 direction = playerCamera ? playerCamera.transform.forward : shootOrigin.forward;

        Vector3 start = shootOrigin.position;

        Vector3 end;
        if (Physics.Raycast(origin, direction, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            end = hit.point;

            if (hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
                enemy.TakeDamage(damage);
        }
        else
        {
            end = origin + direction * range;
        }

        // Spawn tracer
        if (tracerPrefab)
        {
            var tracer = Instantiate(tracerPrefab);
            tracer.Init(start, end);
        }

    }
}
