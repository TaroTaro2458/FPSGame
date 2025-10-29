using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] int pelletCount = 8;           // ���˂���e�̐�
    [SerializeField] float spreadAngle = 10f;       // �g�U�p�x�i�x�j
    [SerializeField] float bulletSpeed = 20f;

    void Update()
    {
        // ���N���b�N�Ŕ���
        if (Input.GetButtonDown("Fire1"))
        {
            FireShotgun();
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
            bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}

