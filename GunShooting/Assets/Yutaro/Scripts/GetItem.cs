using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GetItem : MonoBehaviour
{
    // Inspector�ŕ���̐ݒ�A���킲�Ƃ�Prefab���A�^�b�`
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // �����ʒu
    [SerializeField] Transform handTransform;
    // �ő呕����
    [SerializeField] int maxItems = 2;
    private List<GameObject> currentItems = new List<GameObject>();

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
        GameObject item = other.GetComponent<GameObject>();
        // �A�C�e���擾���� tag�Ŏ���
        if (other.CompareTag("fullauto")&& AddItem(item))
        {
            Debug.Log("fullauto");   
            EquipItem(fullAutogunPrefab); // ����Ɠ����ɑ���
            Destroy(other.gameObject); // �A�C�e��������
        }
        else if (other.CompareTag("shotgun") && AddItem(item))
        {
            Debug.Log("shotgun");
            EquipItem(shotgunPrefab); // ����Ɠ����ɑ���
            Destroy(other.gameObject); // �A�C�e��������
        }
        else if (other.CompareTag("single") && AddItem(item))
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

    public bool AddItem(GameObject item)
    {
        if (currentItems.Count >= maxItems)
        {
            Debug.Log("�C���x���g���������ς��ł��I");
            return false;
        }
        currentItems.Add(item);
        return true;
    }
}
