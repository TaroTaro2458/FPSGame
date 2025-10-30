using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    // ŽËŒ‚ˆÊ’u
    [SerializeField] Transform firePoint;
    // ”­ŽË‚·‚é’e‚Ì”
    [SerializeField] int pelletCount = 8;
    // ŠgŽUŠp“xi“xj
    [SerializeField] float spreadAngle = 10f;
    // ’e‚Ì‘¬“x
    [SerializeField] float bulletSpeed = 20f;
    // ˜AŽËŠÔŠui•bj
    [SerializeField] float fireRate = 1.0f; 
    private float nextFireTime = 0f;
    // ’e‚Ìƒ_ƒ[ƒW
    [SerializeField] int bulletDamage = 10;
    // ’e‚Ìƒ_ƒ[ƒW‚ðBulletCnt‚É“n‚·
    BulletCnt bulletCnt;
    void Update()
    {
        // ¶ƒNƒŠƒbƒN‰Ÿ‚µ‘±‚¯‚Ä”­ŽË
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            FireShotgun();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // ŠgŽU•ûŒü‚ðƒ‰ƒ“ƒ_ƒ€‚É¶¬
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

