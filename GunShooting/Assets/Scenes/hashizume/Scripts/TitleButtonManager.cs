using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    void Start()
    {
        // マウスカーソルを表示
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    // ボタンから呼び出す関数
    public void OnStartButton()
    {
        Debug.Log("ゲームスタートボタンが押されました！");
        SceneManager.LoadScene("Main"); // ← 遷移先のシーン名
    }

    public void OnQuitButton()
    {
        Application.Quit(); // 終了（エディタでは無効）
        Debug.Log("Quit Game");
    }
}
