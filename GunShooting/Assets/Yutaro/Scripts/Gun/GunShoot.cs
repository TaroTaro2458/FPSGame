using UnityEngine;

public class GunShoot : MonoBehaviour
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
    BulletCnt bulletCnt;

    void Update()
    {
        // 左クリックで発射
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // 弾を生成して前方に発射
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
}

