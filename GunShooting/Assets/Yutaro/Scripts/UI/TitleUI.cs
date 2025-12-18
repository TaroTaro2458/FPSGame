using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{

    [SerializeField] string playScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isActive = gameObject.activeSelf;
        // マウスカーソルの表示切り替え
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Button_Start()
    {
        SceneManager.LoadScene(playScene);
    }
    public void Button_Quit()
    {
        Application.Quit();
    }
}
