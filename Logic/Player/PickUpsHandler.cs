using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickUpsHandler : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _collectionRadius;

    private void Awake()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = _collectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Coin>(out Coin coin))
        {
            CollectCoin(coin);
        }
    }

    public event Action<int> CoinCollected = delegate { };

    private void CollectCoin(Coin coin)
    {
        int value = coin.Collect();
        CoinCollected.Invoke(value);
    }
}