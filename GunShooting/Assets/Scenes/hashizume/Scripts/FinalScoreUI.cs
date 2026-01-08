using UnityEngine;
using TMPro;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text finalScoreText;

    void Start()
    {
        int score = ScoreManager.Instance.GetScore();
        finalScoreText.text = "FINAL SCORE : " + score;
    }
}
