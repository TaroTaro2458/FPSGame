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
    [SerializeField] int bulletDamage = 10;
    // �e�̃_���[�W��BulletCnt�ɓn��
    BulletCnt bulletCnt;

    // Overheat�N���X
    Overheat overheat;
    // 1��������̃Q�[�W�㏸��
    [SerializeField] float heatPerShot = 5f;

    private void Start()
    {
        // Overheat�N���X�̎Q�Ƃ��擾
        overheat = FindObjectOfType<Overheat>();
    }
    void Update()
    {
        // ���N���b�N�����������Ă���ԁA���Ԋu�Ŕ���
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && overheat.CanFire)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
            overheat.RegisterShot(heatPerShot);
        }
    }

    void Fire()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f); 
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        bulletCnt = bullet.GetComponent<BulletCnt>();
        bulletCnt.playerBulletDamage = bulletDamage;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}

