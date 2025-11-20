using UnityEngine;

public class GunTest : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float fireRate = 0.5f;
    private float nextFireTime = 0f;

    [SerializeField] int bulletDamage = 10;
    BulletCnt bulletCnt;
    [SerializeField] float heatPerShot = 10f;

    // Overheatクラス
    private OverHeatTest overheat;

    private void Start()
    {
        // Overheatを検索
        overheat = FindFirstObjectByType<OverHeatTest>();
    }

    void Update()
    {
        // 射撃可能 && 連射間隔クリア && 左クリック押された
        if (Input.GetButton("Fire1") &&
            Time.time >= nextFireTime &&
            overheat != null &&
            overheat.CanFire)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;

            // ★ 引数なしでOK（内部の heatPerShot を使用）
            overheat.RegisterShot(heatPerShot);
        }
    }

    void Shoot()
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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}
