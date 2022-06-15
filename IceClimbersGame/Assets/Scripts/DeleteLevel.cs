//Script que controla la desaparición de los niveles al llegar al fondo de la escena
//Creado por Alexis Alvarado.
//Fecha: 02/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLevel : MonoBehaviour
{
    private Transform despwnerinador;

    //Localiza la coordenada y de eliminación
    private void Start()
    {
        despwnerinador = GameObject.FindObjectOfType<LevelGenerator>().despawnerinador;
    }


    //Detecta si el nivel está debajo de la coordenada de eliminación y lo elimina de ser cierto
    void Update()
    {
        if (transform.position.y < despwnerinador.position.y)
        {
            Destroy(this.gameObject);
        }  
    }
}
