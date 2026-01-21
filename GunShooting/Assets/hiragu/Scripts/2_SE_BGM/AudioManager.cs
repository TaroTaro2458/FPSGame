using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SE")]
    [SerializeField] List<SEData> seList;

    [Header("BGM")]
    [SerializeField] List<BGMData> bgmList;

    AudioSource seSource;
    AudioSource bgmSource;

    Dictionary<string, BGMData> bgmDict;
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

        // BGM用
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.spatialBlend = 0f;

        // Dictionary化（SEDataごと保持）
        seDict = new Dictionary<SEType, SEData>();
        foreach (var se in seList)
        {
            seDict[se.type] = se;
        }

        bgmDict = new Dictionary<string, BGMData>();
        foreach (var bgm in bgmList)
            bgmDict[bgm.name] = bgm;

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

    // これ以下はBGM用のメソッド
    public void PlayBGM(string name)
    {
        Debug.Log("bgm");
        if (!bgmDict.TryGetValue(name, out var bgm))
        {
            Debug.LogWarning($"BGM {name} が見つかりません");
            return;
        }

        if (bgmSource.clip == bgm.clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = bgm.clip;
        bgmSource.volume = bgm.volume;
        bgmSource.loop = bgm.loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void FadeOutBGM(float duration = 1f)
    {
        StartCoroutine(FadeOut(duration));
    }

    System.Collections.IEnumerator FadeOut(float duration)
    {
        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = startVolume;
    }
}
