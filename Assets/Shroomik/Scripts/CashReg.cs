using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CashReg : MonoBehaviour
{
    public static Action OnCustomerApprouched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            OnCustomerApprouched?.Invoke();
        }
    }


}
