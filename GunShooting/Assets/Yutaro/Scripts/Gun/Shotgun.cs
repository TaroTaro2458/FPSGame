using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] int pelletCount = 8;           // ”­Ë‚·‚é’e‚Ì”
    [SerializeField] float spreadAngle = 10f;       // ŠgUŠp“xi“xj
    [SerializeField] float bulletSpeed = 20f;

    void Update()
    {
        // ¶ƒNƒŠƒbƒN‚Å”­Ë
        if (Input.GetButtonDown("Fire1"))
        {
            FireShotgun();
        }
    }

    void FireShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // ŠgU•ûŒü‚ğƒ‰ƒ“ƒ_ƒ€‚É¶¬
            Vector3 spreadDir = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0) * firePoint.forward;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(spreadDir));
            bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}

