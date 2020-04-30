using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<GameObject> CoinPickuped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement player))
        {
            CoinPickuped?.Invoke(gameObject);
        }
    }
}