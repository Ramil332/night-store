using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductFall : MonoBehaviour
{
    public List<GameObject> Products;
    private int _num = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Products[_num].transform.eulerAngles.y > 90)
            {

                Vector3 currentRotation = Products[_num].transform.eulerAngles;
                Products[_num].transform.eulerAngles = new Vector3(-30, currentRotation.y, currentRotation.z);
                _num++;
                Debug.Log("1");
            }
            else if(Products[_num].transform.eulerAngles.y < 90)
            {
                Vector3 currentRotation = Products[_num].transform.eulerAngles;
                Products[_num].transform.eulerAngles = new Vector3(-30, currentRotation.y, currentRotation.z);
                _num++;
                Debug.Log("2");
            }
        }
        
    }

            
        
    
    
}
