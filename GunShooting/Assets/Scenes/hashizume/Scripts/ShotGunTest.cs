using UnityEngine;

public class ShotgunTest : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] int pelletCount = 8;
    [SerializeField] float spreadAngle = 10f;
    [SerializeField] float bulletSpeed = 20f;

    [SerializeField] float fireRate = 1.0f;
    private float nextFireTime = 0f;

    [SerializeField] int bulletDamage = 10;
    private BulletCnt bulletCnt;
    [SerializeField] float heatPerShot = 10f;

    // Overheat システム
    private OverHeatTest overheat;

    private void Start()
    {
        // 新しい推奨API
        overheat = FindFirstObjectByType<OverHeatTest>();
    }

    void Update()
    {
        // 射撃条件
        if (Input.GetButton("Fire1") &&
            Time.time >= nextFireTime &&
            overheat != null &&
            overheat.CanFire)
        {
            FireShotgun();
            nextFireTime = Time.time + fireRate;

            // ★引数なし（Overheat内部の heatPerShot が使われる）
            overheat.RegisterShot(heatPerShot);
        }
    }

    void FireShotgun()
    {
        Transform camTransform = Camera.main.transform;

        for (int i = 0; i < pelletCount; i++)
        {
            // 拡散方向
            Quaternion spreadRotation =
                camTransform.rotation *
                Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0
                );

            Vector3 spreadDir = spreadRotation * Vector3.forward;

            // 弾生成
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(spreadDir)
            );

            bulletCnt = bullet.GetComponent<BulletCnt>();
            bulletCnt.playerBulletDamage = bulletDamage;

            // 弾を飛ばす
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(spreadDir * bulletSpeed, ForceMode.Impulse);
        }
    }
}
