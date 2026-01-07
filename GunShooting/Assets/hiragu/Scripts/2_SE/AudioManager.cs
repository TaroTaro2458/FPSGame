using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SE Clips")]
    [SerializeField] List<SEData> seList;

    AudioSource seSource;
    Dictionary<SEType, AudioClip> seDict;

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

        seSource = gameObject.AddComponent<AudioSource>();

        // Dictionary‰»
        seDict = new Dictionary<SEType, AudioClip>();
        foreach (var se in seList)
        {
            seDict[se.type] = se.clip;
        }
    }

    public void PlaySE(SEType type)
    {
        if (seDict.TryGetValue(type, out var clip))
        {
            Debug.Log("ŒÄ‚Î‚ê‚½");
            seSource.PlayOneShot(clip);
        }
    }

    public void PlaySE3D(SEType type, Vector3 position)
    {
        if (seDict.TryGetValue(type, out var clip))
        {
            Debug.Log("ŒÄ‚Î‚ê‚½");
            AudioSource.PlayClipAtPoint(clip, position, 2f);
        }
    }
}
