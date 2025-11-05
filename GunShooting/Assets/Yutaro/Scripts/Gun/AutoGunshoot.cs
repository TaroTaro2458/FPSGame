using UnityEngine;

public class AutoGunshoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    // 連射間隔（秒）
    [SerializeField] float fireRate = 0.2f; 
    private float nextFireTime = 0f;
    // 弾のダメージ
    [SerializeField] int bulletDamage = 10;
    // 弾のダメージをBulletCntに渡す
    BulletCnt bulletCnt;

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
        // 左クリックを押し続けている間、一定間隔で発射
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && overheat.CanFire)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
            overheat.RegisterShot(heatPerShot);
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}

