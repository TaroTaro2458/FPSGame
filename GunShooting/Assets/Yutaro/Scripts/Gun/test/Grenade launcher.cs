using UnityEngine;

public class Grenadelauncher : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    //射撃位置
    [SerializeField] Transform firePoint;
    //弾の速度
    [SerializeField] float bulletSpeed = 20f;
    // 連射間隔（秒）
    [SerializeField] float fireRate = 0.5f;
    private float nextFireTime = 0f;
    // 弾のダメージ
    [SerializeField] int bulletDamage = 10;
    // 弾のダメージをBulletCntに渡す
    explosionBulletCnt bulletCnt;

    // Overheatクラス
    Overheat overheat;
    // 1発あたりのゲージ上昇量
    [SerializeField] float heatPerShot = 5f;

    private void Start()
    {
        // Overheatクラスの参照を取得
        overheat = FindObjectOfType<Overheat>();
    }

    void Update()
    {
        // 左クリックで発射
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && overheat.CanFire)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            overheat.RegisterShot(heatPerShot);
        }
    }

    void Shoot()
    {
        // カメラの中央からレイを飛ばしてターゲットを決定
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

        // グレネードを生成
        GameObject grenade = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bulletCnt = grenade.GetComponent<explosionBulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // 放物線を描くように力を加える（斜め上方向に）
        Vector3 launchDirection = (shootDirection + Vector3.up * 0.3f).normalized;
        rb.AddForce(launchDirection * bulletSpeed, ForceMode.Impulse);
    }
}
