using UnityEngine;
using TMPro;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text rankText;

    void Start()
    {
        int score = ScoreManager.Instance.GetScore();
        finalScoreText.text = "FINAL SCORE : " + score;

        var rank = ScoreManager.Instance.GetRank();
        rankText.text = "RANK : " + rank.ToString();
    }
}

