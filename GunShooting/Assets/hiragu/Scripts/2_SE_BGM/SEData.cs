using UnityEngine;

[System.Serializable]
public class SEData
{
    public SEType type;
    public AudioClip clip;

    [Range(0f, 2f)]
    public float volume = 1f;
}
