using UnityEngine;

public class AutoGunshoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    // �A�ˊԊu�i�b�j
    [SerializeField] float fireRate = 0.2f; 
    private float nextFireTime = 0f;
    // �e�̃_���[�W
    [SerializeField] float bulletDamage = 10f;

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

