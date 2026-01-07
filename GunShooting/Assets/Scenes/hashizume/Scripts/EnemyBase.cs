using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int scoreValue = 10; // “G‚²‚Æ‚Ì“_”

    public virtual void Die()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
