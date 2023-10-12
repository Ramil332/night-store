using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _lights;
    private bool _isBroken;

    public bool IsBroke()
    {
        return _isBroken;
    }
    private void OnEnable()
    {
        GlobalEvents.OnGeneratorBroke += GeneratorBroke;
        _lights.SetActive(true);
    }

    private void OnDisable()
    {
        GlobalEvents.OnGeneratorBroke -= GeneratorBroke;
    }

    private void GeneratorBroke()
    {
        _lights.SetActive(false);
        _isBroken = true;
    }
    
    public void GeneratorFixed()
    {
        _isBroken = false;
        _lights.SetActive(true);

    }
}
