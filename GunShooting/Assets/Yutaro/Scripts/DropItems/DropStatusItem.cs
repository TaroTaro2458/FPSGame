using UnityEngine;

public class DropStatusItem : MonoBehaviour
{
    [SerializeField] AbilitySelectionUI abilityUI; // UIの参照をInspectorで設定

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Status Item Picked Up");
            abilityUI.ShowFixedAbilities();
            Destroy(gameObject); // アイテムを消す
        }
    }
}
