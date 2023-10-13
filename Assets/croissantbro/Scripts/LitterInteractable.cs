using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterInteractable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Destroy(this.gameObject);
    }

}
