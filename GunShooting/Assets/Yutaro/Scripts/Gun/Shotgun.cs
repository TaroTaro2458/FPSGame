using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    // 射撃位置
    [SerializeField] Transform firePoint;
    // 発射する弾の数
    [SerializeField] int pelletCount = 8;
    // 拡散角度（度）
    [SerializeField] float spreadAngle = 10f;
    // 弾の速度
    [SerializeField] float bulletSpeed = 20f;
    // 連射間隔（秒）
    [SerializeField] float fireRate = 1.0f; 
    private float nextFireTime = 0f;
    // 弾のダメージ
    [SerializeField] int bulletDamage = 10;
    // 弾のダメージをBulletCntに渡す
    BulletCnt bulletCnt;
    // Overheatクラス
     Overheat overheat;
    // 1発あたりのゲージ上昇量
    [SerializeField] float heatPerShot = 10f;

    private void Start()
    {
        // Overheatクラスの参照を取得
        overheat = FindObjectOfType<Overheat>();
    }
    void Update()
    {

        // 左クリック押し続けて発射
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime&& overheat.CanFire)
        {
            FireShotgun();
            nextFireTime = Time.time + fireRate;
            overheat.RegisterShot(heatPerShot);
        }
    }

    void FireShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // 拡散方向をランダムに生成
            Vector3 spreadDir = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0) * firePoint.forward;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(spreadDir));
            bulletCnt = bullet.GetComponent<BulletCnt>();
            bulletCnt.playerBulletDamage = bulletDamage;
            bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}

