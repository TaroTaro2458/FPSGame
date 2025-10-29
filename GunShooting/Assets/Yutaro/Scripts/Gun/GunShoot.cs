using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    //�ˌ��ʒu
    [SerializeField] Transform firePoint;
    //�e�̑��x
    [SerializeField] float bulletSpeed = 20f;
    �@

    void Update()
    {
        // ���N���b�N�Ŕ���
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �e�𐶐����đO���ɔ���
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
}

