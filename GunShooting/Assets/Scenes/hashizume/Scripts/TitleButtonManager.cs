using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    // �{�^������Ăяo���֐�
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene"); // �� �J�ڐ�̃V�[����
    }

    public void OnQuitButton()
    {
        Application.Quit(); // �I���i�G�f�B�^�ł͖����j
        Debug.Log("Quit Game");
    }
}
