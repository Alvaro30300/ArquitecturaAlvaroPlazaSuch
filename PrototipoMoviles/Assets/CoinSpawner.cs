using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public float timeRespawn;

    private float randomX;
    private float randomY;

    private void Start()
    {
        StartCoroutine(Respawn());
    }

  

    IEnumerator Respawn()
    {
        randomX = Random.Range(-2, 2);
        randomY = Random.Range(-3, 1);
        GameObject coin = CoinGenerator.Clone(new Vector3(randomX,randomY,-1));
        coin.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(timeRespawn);

        StartCoroutine(Respawn());
    }
}
