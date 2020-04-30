using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

public class Coin : MonoBehaviour
{
    public event UnityAction<GameObject> CoinPickuped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() !=null)
        {
            CoinPickuped?.Invoke(gameObject);
        }
    }
}