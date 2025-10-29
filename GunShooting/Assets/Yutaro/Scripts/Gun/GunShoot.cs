using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    //射撃位置
    [SerializeField] Transform firePoint;
    //弾の速度
    [SerializeField] float bulletSpeed = 20f;
    　

    void Update()
    {
        // 左クリックで発射
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 弾を生成して前方に発射
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
}

