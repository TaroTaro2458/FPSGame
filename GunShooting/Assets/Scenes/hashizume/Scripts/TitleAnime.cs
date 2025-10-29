using UnityEngine;
using UnityEngine.UI;

public class TitleLogoPop : MonoBehaviour
{
    public Image logo;

    void Start()
    {
        // logo が未設定なら自動取得
        if (logo == null)
        {
            logo = GetComponent<Image>();
        }

        // 最初は透明
        logo.canvasRenderer.SetAlpha(0f);

        // 1秒かけてフェードイン
        logo.CrossFadeAlpha(1f, 1.0f, false);
    }
}
