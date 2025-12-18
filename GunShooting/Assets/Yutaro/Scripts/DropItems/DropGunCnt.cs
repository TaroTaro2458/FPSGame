using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


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
   /* void Update()
    {
        if (getItem == null)
        {
            Debug.LogWarning("getItem が null です。プレイヤーオブジェクトが見つからなかった可能性があります。");
            return;
        }
        // 取得したアイテムリストを更新
        ItemData data = gameObject.GetComponent<ItemData>();
        droppedItems = getItem.GetCurrentItems();
        // 所持しているアイテムにドロップアイテムが含まれていなければ消去
        if (data != null && !droppedItems.Contains(data.itemName))
        {
            Destroy(gameObject);
        }
    }*/

    void OnEnable()
    {
        if (getItem != null)
            getItem.onItemChanged.AddListener(CheckDrop);
    }

    void OnDisable()
    {
        if (getItem != null)
            getItem.onItemChanged.RemoveListener(CheckDrop);
    }

    void CheckDrop()
    {
        ItemData data = GetComponent<ItemData>();
        if (data != null && !getItem.GetCurrentItems().Contains(data.itemName))
        {
            Destroy(gameObject);
        }

    }
}
