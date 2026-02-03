using System.Threading.Tasks;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField]  float lifetime = 50f; // オブジェクトの寿命（秒）
    private float timer = 0f; // 経過時間を追跡するタイマー

    // Update is called once per frame
    void Update()
    {
        if (timer > lifetime)
        {
            //オブジェクトを消去
            Destroy(gameObject);
        }
        timer += Time.deltaTime; // 経過時間を更新
        
    }
}
