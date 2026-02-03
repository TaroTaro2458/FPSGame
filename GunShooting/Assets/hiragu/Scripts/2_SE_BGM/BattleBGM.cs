using UnityEngine;

public class BattleBGM : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    bool isGameOverBGMPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
            return;
        }

        AudioManager.Instance.PlayBGM(BGMType.Battle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOverBGMPlayed && playerHealth.CurrentHealth <= 0)
        {
            isGameOverBGMPlayed = true;

            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayBGM(BGMType.GameOver);
        }
    }
}
