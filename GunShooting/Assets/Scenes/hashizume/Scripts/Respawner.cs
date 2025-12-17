using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
    public void StartRespawn(float time, GameObject target)
    {
        StartCoroutine(RespawnCoroutine(time, target));
    }

    private IEnumerator RespawnCoroutine(float time, GameObject target)
    {
        yield return new WaitForSeconds(time);
        target.SetActive(true);
    }
}
