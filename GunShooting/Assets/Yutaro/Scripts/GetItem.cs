using System;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    // Inspector�ŕ���̐ݒ�A���킲�Ƃ�Prefab���쐬
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // �����ʒu
    [SerializeField] Transform handTransform; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // �A�C�e���擾���� tag�Ŏ���
        if (other.CompareTag("fullauto"))
        {
            Debug.Log("fullauto");
            EquipItem(fullAutogunPrefab); // ����Ɠ����ɑ���
            Destroy(other.gameObject); // �A�C�e��������
        }
        else if (other.CompareTag("shotgun"))
        {
            Debug.Log("shotgun");
            EquipItem(shotgunPrefab); // ����Ɠ����ɑ���
            Destroy(other.gameObject); // �A�C�e��������
        }
        else if (other.CompareTag("single"))
        {
            Debug.Log("handgun");
            EquipItem(handgunPrefab); // ����Ɠ����ɑ���
            Destroy(other.gameObject); // �A�C�e��������
        }
    }

    void EquipItem(GameObject itemPrefab)
    {
        Transform equipSlot = handTransform; // �����ʒu
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }
}
