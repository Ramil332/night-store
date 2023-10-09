using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterInteractable : MonoBehaviour
{
    public void Interact()
    {
        Destroy(gameObject);
    }

}
