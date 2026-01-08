using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    int score = 0;

    [Header("UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;

    [Header("Combo")]
    [SerializeField] float comboTimeLimit = 3f;
    [SerializeField] float baseComboSize = 40f;
    [SerializeField] float sizeGrowPerCombo = 6f;

    int combo = 0;
    float comboTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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

    public void AddScore(int baseScore)
    {
        combo++;
        comboTimer = comboTimeLimit;

        int finalScore = baseScore * combo;
        score += finalScore;

        UpdateUI();
        UpdateComboUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE : " + score;
    }

    void UpdateComboUI()
    {
        if (comboText == null) return;

        if (combo <= 1)
        {
            comboText.text = "";
        }
        else
        {
            comboText.text = combo + " COMBO!";
            float size = baseComboSize + (combo - 1) * sizeGrowPerCombo;
            comboText.fontSize = size;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        combo = 0;
        UpdateUI();
        UpdateComboUI();
    }
}
