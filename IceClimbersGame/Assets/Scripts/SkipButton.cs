//Script que controla el bot�n de saltar en la pantalla de historia
//Creado por Alexis Alvarado.
//Fecha: 03/05/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    //Detecta si es la primera vez que el jugador entra desde el bot�n de jugar y elimina el bot�n de saltar 
    //de no ser as�
    
    void Start()
    {
        if(PlayerPrefs.GetInt("primerJuego") == 0 && PlayerPrefs.GetInt("ultimaEscena") == 1)
        {
            PlayerPrefs.SetInt("primerJuego", 1);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.GetInt("ultimaEscena") == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
