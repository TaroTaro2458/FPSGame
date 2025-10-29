using UnityEngine;
using UnityEngine.UI;

public class TitleLogoPop : MonoBehaviour
{
    public Image logo;

    void Start()
    {
        // logo �����ݒ�Ȃ玩���擾
        if (logo == null)
        {
            logo = GetComponent<Image>();
        }

        // �ŏ��͓���
        logo.canvasRenderer.SetAlpha(0f);

        // 1�b�����ăt�F�[�h�C��
        logo.CrossFadeAlpha(1f, 1.0f, false);
    }
}
