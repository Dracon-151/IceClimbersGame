//Script que controla el movimiento del enemigo sierra
//Creado por Alexis Alvarado.
//Fecha: 03/05/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamonSpin : MonoBehaviour
{
    //Rota al enemigo a 4 grados por frame
    void Update()
    {
        transform.Rotate(0,0,-4 * Time.timeScale);
    }
}
