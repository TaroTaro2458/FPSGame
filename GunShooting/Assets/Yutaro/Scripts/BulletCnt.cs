using UnityEngine;

public class BulletCnt : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�T�b��Ɏ����I�ɏ���
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        //CompareTag("Enemy") ||
        // �G�Ȃǂɓ��������Ƃ���������
        if ( other.gameObject.CompareTag("ground"))
        {
            
            //�Փ˂����瑦���ɏ���
            Destroy(gameObject);
        }

    }

}
