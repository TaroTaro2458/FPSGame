using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    // ボタンから呼び出す関数
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene"); // ← 遷移先のシーン名
    }

    public void OnQuitButton()
    {
        Application.Quit(); // 終了（エディタでは無効）
        Debug.Log("Quit Game");
    }
}
