using UnityEngine;

public class DropStatusItem : MonoBehaviour
{
    [SerializeField] GameObject abilityUIPrefab;
    //HierarchyにBossDropRootを追加して参照
    [SerializeField] Transform abilityCanvasPos;

    /*void Start()
    {
        abilityCanvasPos = GameObject.Find("BossDrop_UI").transform;
        var obj = GameObject.Find("BossDrop_UI");
        Debug.Log(obj);

        if (obj == null)
            Debug.LogError("BossDrop_UI not found!");

        abilityCanvasPos = obj.transform;
    }*/


    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Status Item Picked Up");

            GameObject uiInstance = Instantiate(abilityUIPrefab, abilityCanvasPos.position, Quaternion.identity, abilityCanvasPos);

            // AbilitySelectionUI を取得
            var abilityUI = uiInstance.GetComponent<AbilitySelectionUI>();

            Debug.Log("uiInstance name = " + uiInstance.name);

            Debug.Log("Components on uiInstance root:");
            foreach (var c in uiInstance.GetComponents<Component>())
            {
                Debug.Log(c.GetType().Name);
            }

            if (abilityUI != null)
            {
                Debug.LogError("AbilitySelectionUI が Prefab の Root に付いていません");
                // 子オブジェクトから AbilityButton を全部取得！
                abilityUI.abilityButtons = uiInstance.GetComponentsInChildren<AbilityButton>();

                // 表示処理
                abilityUI.ShowFixedAbilities();
                Debug.Log("AbilitySelectionUI = " + abilityUI);
            }


            Destroy(gameObject); // アイテムを消す
        }
    }
}
