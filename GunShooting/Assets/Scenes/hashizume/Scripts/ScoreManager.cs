using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    int score = 0;
    [SerializeField] TMP_Text scoreText;　//スコア
    [SerializeField] float comboTimeLimit = 3f;//コンボが切れる時間
    [SerializeField] TMP_Text comboText;//コンボ用

    int combo = 0;
    float comboTimer;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int baseScore)
    {
        combo++;
        comboTimer = comboTimeLimit;

        int finalScore = baseScore * combo;

        score += finalScore;
        UpdateUI();
        UpdateComboUI();
    }

    void Update()
    {
        if (combo > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                combo = 0;
                UpdateComboUI();
            }
        }
    }



    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE : " + score;
    }

    public int GetScore()
    {
        return score;
    }

    //コンボ用
    void UpdateComboUI()
    {
        if (comboText == null) return;

        if (combo <= 1)
            comboText.text = "";
        else
            comboText.text = combo + " COMBO!";
    }


    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
