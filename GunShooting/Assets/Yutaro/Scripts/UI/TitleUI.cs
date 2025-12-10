using UnityEngine;

public class TitleUI : MonoBehaviour
{

    [SerializeField] string playScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(playScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
