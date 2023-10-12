using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _pfbCustomer;

    [SerializeField] private Transform[] _spawnPoint;

    private void OnEnable()
    {
        GlobalEvents.OnCustomerSpawn += SpawnCustomer;
    }

    private void OnDisable()
    {
        GlobalEvents.OnCustomerSpawn -= SpawnCustomer;
    }
    private void SpawnCustomer()
    {
        int numC = Random.Range(0, _pfbCustomer.Length); 
        int numS = Random.Range(0, _spawnPoint.Length); 
        Instantiate(_pfbCustomer[numC], _spawnPoint[numS]);
    }
}
