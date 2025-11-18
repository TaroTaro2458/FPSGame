using UnityEngine;

public class AutoGunTest : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;

    [SerializeField] float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [SerializeField] int bulletDamage = 10;
    BulletCnt bulletCnt;
    [SerializeField] float heatPerShot = 10f;

    // Overheat クラス
    private OverHeatTest overheat;

    private void Start()
    {
        // Overheat の参照取得（新しい推奨API）
        overheat = FindFirstObjectByType<OverHeatTest>();
    }

    void Update()
    {
        // 押しっぱなし射撃
        if (Input.GetButton("Fire1") &&
            Time.time >= nextFireTime &&
            overheat != null &&
            overheat.CanFire)
        {
            Fire();
            nextFireTime = Time.time + fireRate;

            // ★引数なし（内部の heatPerShot が使われる）
            overheat.RegisterShot(heatPerShot);
        }
    }

    void Fire()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}
