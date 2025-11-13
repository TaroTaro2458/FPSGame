using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropGunCnt : MonoBehaviour
{
    //getItemスクリプトの参照
    GetItem getItem;
    private List<string> droppedItems = new List<string>();
    [SerializeField] string playerName = "Player(1)";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerオブジェクトを名前で検索
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            getItem = playerObj.GetComponent<GetItem>();
        }
        else
        {
            Debug.LogError("プレイヤーオブジェクトが見つかりません: " + playerName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (getItem == null)
        {
            Debug.LogWarning("getItem が null です。プレイヤーオブジェクトが見つからなかった可能性があります。");
            return;
        }
        ItemData data = gameObject.GetComponent<ItemData>();
        droppedItems = getItem.GetCurrentItems();

        if (data != null && !droppedItems.Contains(data.itemName))
        {
            Destroy(gameObject);
        }
    }

}
