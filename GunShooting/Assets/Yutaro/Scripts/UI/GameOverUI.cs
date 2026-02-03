using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // メインゲームのシーン名をインスペクターで設定
    [SerializeField] string playScene;
    void Start()
    {
        bool isActive = gameObject.activeSelf;
        // マウスカーソルの表示切り替え
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // リトライボタンが押されたときに呼ばれるメソッド
    public void Retry()
    {
        AudioManager.Instance.PlaySE(SEType.NextButton);    // ボタンを押したときのSE
        SceneManager.LoadScene(playScene); // メインゲームのシーン名
    }

    // クイットボタンが押されたときに呼ばれるメソッド
    public void Quit()
    {
        AudioManager.Instance.PlaySE(SEType.CancelButton);  // ボタンを押したときのSE
        Application.Quit(); // ゲーム終了
    }
}

