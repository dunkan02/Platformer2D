using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinController : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int _numberPickupCoins = 0;
    private List<Transform> _spawnPoints = new List<Transform>();
    private List<GameObject> _coins = new List<GameObject>();
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(15f);

    private void Awake()
    {
        var points = gameObject.GetComponentsInChildren<CoinSpawnPoint>();
        foreach (CoinSpawnPoint point in points)
        {
            _spawnPoints.Add(point.transform);
        }
    }

    private void OnEnable ()
    {
        foreach (Transform transformCoin in _spawnPoints)
        {
            GameObject tempGameObject= Instantiate(coinPrefab, transformCoin.position, Quaternion.identity, transform);
            tempGameObject.GetComponent<Coin>().CoinPickuped += OnPickupCoin;

            _coins.Add(tempGameObject);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject coin in _coins)
        {
            coin.GetComponent<Coin>().CoinPickuped -= OnPickupCoin;
        }
    }

    private void OnPickupCoin(GameObject coin)
    {
        _numberPickupCoins++;
        StartCoroutine(TimerToReactivateCoin(coin));
    }

    private IEnumerator TimerToReactivateCoin(GameObject coin)
    {
        coin.SetActive(false);
        yield return _waitForSeconds;
        coin.SetActive(true);
    }
}
