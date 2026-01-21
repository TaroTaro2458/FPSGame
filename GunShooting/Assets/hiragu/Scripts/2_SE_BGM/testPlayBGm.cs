using UnityEngine;

public class testPlayBGm : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayBGM("battle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
