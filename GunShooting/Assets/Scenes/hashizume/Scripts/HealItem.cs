using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 30;
    public float rotateSpeed = 50f;
    public float respawnTime = 5f;

    private Transform parentObj; // 親を保存


    void Awake()
    {
        parentObj = transform.parent;
    }

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();

            if (hp != null)
                hp.Heal(healAmount);

            // モデル（自分）だけを消す
            gameObject.SetActive(false);

           

            // 親からリスポーンを開始する
            parentObj.GetComponent<Respawner>().StartRespawn(respawnTime, gameObject);
        }
    }
}
