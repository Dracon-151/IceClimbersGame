//Script que controla la aparición del easter egg de la pantalla creditos
//Creado por Daniel Sepúlveda.
//Fecha: 11/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDani : MonoBehaviour
{
    [SerializeField] private GameObject img;
    private bool estado = false;

    //Invierte el estado de visibilidad del easter egg
    public void onOff()
    {
        estado = !estado;

        img.SetActive(estado);
    }
}
