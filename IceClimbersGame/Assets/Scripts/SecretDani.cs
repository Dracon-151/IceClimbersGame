using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDani : MonoBehaviour
{
    [SerializeField] private GameObject img;
    private bool estado = false;

    public void onOff()
    {
        estado = !estado;

        img.SetActive(estado);
    }
}
