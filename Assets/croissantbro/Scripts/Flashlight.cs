using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isOn = false;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            if(isOn)
            light.SetActive(true); 
            else light.SetActive(false);
        }
    }
}
