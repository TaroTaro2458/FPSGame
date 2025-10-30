using UnityEngine;

public class AutoGunshoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    // ˜AŽËŠÔŠui•bj
    [SerializeField] float fireRate = 0.2f; 
    private float nextFireTime = 0f;
    // ’e‚Ìƒ_ƒ[ƒW
    [SerializeField] int bulletDamage = 10;
    // ’e‚Ìƒ_ƒ[ƒW‚ðBulletCnt‚É“n‚·
    BulletCnt bulletCnt;

    void Update()
    {
        // ¶ƒNƒŠƒbƒN‚ð‰Ÿ‚µ‘±‚¯‚Ä‚¢‚éŠÔAˆê’èŠÔŠu‚Å”­ŽË
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
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

