using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] float stopDistance = 10f;              // 一定距離まで近づく
    [SerializeField] Transform shootingPoint;               // 発射位置
    //[SerializeField] GameObject bulletPrefab;               // 発射する弾
    [SerializeField] float shootingInterval = 3.0f;         // 発射間隔
    [SerializeField] float homingBulletSpeed = 5;
    [SerializeField] float locLanSpeed = 5;
    [SerializeField] List<GameObject> bulletList;

    Transform player;                                       // プレイヤーを追いかけるための位置
    NavMeshAgent agent;                                     // 追いかけるためのもの
    float distance;                                         // 一定距離で追いかけることをやめるためのもの

    GameObject bullet;                                      // これに弾をセット
    Vector3 enemyDirectionControl;                          // 常にプレイヤーのほうを向くため
    float countTime = 0;                                    // 発射間隔制御用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }

        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            //bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            int rand = Random.Range(0, bulletList.Count);
            bullet = Instantiate(bulletList[rand],shootingPoint.position, shootingPoint.rotation);
            LocketLauncherBulletController llbc = bullet.GetComponent<LocketLauncherBulletController>();
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            if(llbc != null)
            {
                llbc.setShootingPoint(shootingPoint);
                llbc.bulletSpeed = locLanSpeed;
            }
            if (hb != null)
            {
                hb.bulletSpeed = homingBulletSpeed;
            }
            
            Destroy(bullet, 5);

            countTime = 0;
        }
    }
}
