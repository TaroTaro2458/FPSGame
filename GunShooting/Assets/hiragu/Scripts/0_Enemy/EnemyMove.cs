using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour, IEnemyDeathListener
{
    Transform player;
    NavMeshAgent agent;
    bool isFootstepPlaying;
    Animator anim;
    bool isDie = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDie) return;

        agent.SetDestination(player.position);


        if (agent.velocity.magnitude > 0.1f)
        {
            if (!isFootstepPlaying)
            {
                AudioManager.Instance.PlaySE3D(SEType.EnemyWalk,transform.position);
                isFootstepPlaying = true;
                Invoke(nameof(ResetFootstep), 10.0f); // 音の長さ
            }
        }
        else
        {
            isFootstepPlaying = false;
        }

    }

    void ResetFootstep()
    {
        isFootstepPlaying = false;
    }

    public void OnDeath()
    {
        isDie = true;

        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        anim.SetTrigger("Die");
        Debug.Log("アニメーション再生");
        agent.enabled = false;
        Destroy(gameObject, 0.8f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySE3D(SEType.SelfDestruction, transform.position);
            Destroy(gameObject);
        }
    }
}
