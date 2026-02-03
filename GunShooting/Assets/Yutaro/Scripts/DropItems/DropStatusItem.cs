using UnityEngine;

public class DropStatusItem : MonoBehaviour
{
    [SerializeField] GameObject abilityUIPrefab;
    //HierarchyにBossDropRootを追加して参照
    [SerializeField] Transform abilityCanvasPos;


    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            /*Debug.Log("Status Item Picked Up");

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


            Destroy(gameObject); // アイテムを消す*/

            if (!collision.gameObject.CompareTag("Player")) return;

            Debug.Log("Status Item Picked Up");

            // すでに存在する AbilitySelectionUI を探す
            var abilityUI = FindObjectOfType<AbilitySelectionUI>(true);

            if (abilityUI == null)
            {
                Debug.LogError("AbilitySelectionUI not found in scene!");
                return;
            }

            Debug.Log("AbilitySelectionUI FOUND");

            // 表示処理
            abilityUI.ShowFixedAbilities();

            Destroy(gameObject); // アイテムを消す

        }
    }
}
