using UnityEngine;

[System.Serializable]
public class BGMData
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    public bool loop = true;
}
