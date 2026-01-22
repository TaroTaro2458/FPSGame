using UnityEngine;

public class DropStatusItem : MonoBehaviour
{
    [SerializeField] GameObject abilityUIPrefab; 
    Transform abilityCanvasPos;

    void Start()
    {
        abilityCanvasPos = GameObject.Find("BossDrop_UI").transform;
    }


    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Status Item Picked Up");

            GameObject uiInstance = Instantiate(abilityUIPrefab, abilityCanvasPos.position, Quaternion.identity, abilityCanvasPos);

            // AbilitySelectionUI を取得
            var abilityUI = uiInstance.GetComponent<AbilitySelectionUI>();

            if (abilityUI != null)
            {
                // 子オブジェクトから AbilityButton を全部取得！
                abilityUI.abilityButtons = uiInstance.GetComponentsInChildren<AbilityButton>();

                // 表示処理
                abilityUI.ShowFixedAbilities();
            }


            Destroy(gameObject); // アイテムを消す
        }
    }
}
