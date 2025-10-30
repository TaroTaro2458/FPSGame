using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    // �ˌ��ʒu
    [SerializeField] Transform firePoint;
    // ���˂���e�̐�
    [SerializeField] int pelletCount = 8;
    // �g�U�p�x�i�x�j
    [SerializeField] float spreadAngle = 10f;
    // �e�̑��x
    [SerializeField] float bulletSpeed = 20f;
    // �A�ˊԊu�i�b�j
    [SerializeField] float fireRate = 1.0f; 
    private float nextFireTime = 0f;
    // �e�̃_���[�W
    [SerializeField] int bulletDamage = 10;
    // �e�̃_���[�W��BulletCnt�ɓn��
    BulletCnt bulletCnt;
    void Update()
    {
        // ���N���b�N���������Ĕ���
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
            // �g�U�����������_���ɐ���
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

