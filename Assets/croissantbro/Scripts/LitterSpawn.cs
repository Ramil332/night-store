using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LitterSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPointList;
    [SerializeField] private Transform[] _litter;




    private void OnEnable()
    {
        GlobalEvents.OnSpawnLitter += SpawnTrash;
    }
    private void OnDisable()
    {
        GlobalEvents.OnSpawnLitter -= SpawnTrash;
    }

    private void SpawnTrash()
    {
        int _numLitter = Random.Range(0, _litter.Length);
        int _numSpawn = Random.Range(0, _spawnPointList.Length);
        Instantiate(_litter[_numLitter], _spawnPointList[_numSpawn].transform.position, Quaternion.identity);
    }
}
