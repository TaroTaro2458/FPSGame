using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    //�ˌ��ʒu
    [SerializeField] Transform firePoint;
    //�e�̑��x
    [SerializeField] float bulletSpeed = 20f;
    // �A�ˊԊu�i�b�j
    [SerializeField] float fireRate = 0.5f; 
    private float nextFireTime = 0f;
    // �e�̃_���[�W
    [SerializeField] int bulletDamage = 10;
    // �e�̃_���[�W��BulletCnt�ɓn��
    BulletCnt bulletCnt;

    void Update()
    {
        // ���N���b�N�Ŕ���
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // �e�𐶐����đO���ɔ���
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
}

