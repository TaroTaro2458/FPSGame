using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    private GameState currentState;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        switch (newState)
        {
            case GameState.Title:
                Debug.Log("State changed to Title");
                AudioManager.Instance.StopBGM();
                AudioManager.Instance.PlayBGM(BGMType.Title);
                break;

            case GameState.Battle:
                AudioManager.Instance.StopBGM();
                AudioManager.Instance.PlayBGM(BGMType.Battle);
                break;

            case GameState.GameOver:
                AudioManager.Instance.StopBGM();
                AudioManager.Instance.PlayBGM(BGMType.GameOver);
                break;
        }
    }
}
