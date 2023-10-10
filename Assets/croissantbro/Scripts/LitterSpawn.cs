using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterSpawn : MonoBehaviour
{
    public List<GameObject> SpawnPointList;
    [SerializeField] private GameObject _litter;
    private int _num = 0;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(_litter, SpawnPointList[_num].transform.position, Quaternion.identity);
            _num++;   
        }
    }
}
