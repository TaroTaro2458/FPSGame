using UnityEngine;

public class AutoGunshoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float fireRate = 0.2f; // �A�ˊԊu�i�b�j

    private float nextFireTime = 0f;

    void Update()
    {
        // ���N���b�N�����������Ă���ԁA���Ԋu�Ŕ���
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}

