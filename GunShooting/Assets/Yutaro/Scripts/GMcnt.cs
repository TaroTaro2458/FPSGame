using UnityEngine;

public class GMcnt : MonoBehaviour
{
    void Awake()
    {
        // 60FPSŒÅ’è
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
