using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterInteractable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("���");
        Destroy(this.gameObject);
    }

}
