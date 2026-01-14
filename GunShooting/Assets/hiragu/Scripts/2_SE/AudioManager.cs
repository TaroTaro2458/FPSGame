using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SE Clips")]
    [SerializeField] List<SEData> seList;

    AudioSource seSource;
    Dictionary<SEType, SEData> seDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // SE用 AudioSource
        seSource = gameObject.AddComponent<AudioSource>();
        seSource.playOnAwake = false;
        seSource.spatialBlend = 0f; // 2D用

        // Dictionary化（SEDataごと保持）
        seDict = new Dictionary<SEType, SEData>();
        foreach (var se in seList)
        {
            seDict[se.type] = se;
        }
    }

    // ===== 2D SE（UIなど）=====
    public void PlaySE(SEType type)
    {
        if (seDict.TryGetValue(type, out var se))
        {
            seSource.PlayOneShot(se.clip, se.volume);
        }
        else
        {
            Debug.LogWarning($"SEType {type} が登録されていません");
        }
    }

    // ===== 3D SE（足音・銃声など）=====
    public void PlaySE3D(SEType type, Vector3 position)
    {
        if (!seDict.TryGetValue(type, out var se)) return;

        GameObject go = new GameObject($"SE_{type}");
        go.transform.position = position;

        AudioSource src = go.AddComponent<AudioSource>();
        src.clip = se.clip;
        src.volume = se.volume;
        src.spatialBlend = 1f;   // 3D
        src.minDistance = 2f;
        src.maxDistance = 25f;

        src.Play();
        Destroy(go, se.clip.length);
    }
}
