//Script que controla la dificultad de los niveles
//Creado por Alexis Alvarado y Eduardo Gonzalez.
//Fecha: 08/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificulty : MonoBehaviour
{
    [SerializeField] float puntos;
    private float probabilidad;
    private float score;

    void Start()
    {
        //Se obtiene la puntuación actual del jugador
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score;

        //Se calcula la probabilidad de aparición del objeto en relación a la puntuación del jugador
        if (score <= puntos)
        {
            Destroy(this.gameObject);
        }
        else
        {
            probabilidad = Random.Range(0, 100);
            if (probabilidad > (score / 25) - (puntos/80))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
