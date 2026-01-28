using UnityEngine;

public class testPlayBGm : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayBGM(BGMType.Battle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
