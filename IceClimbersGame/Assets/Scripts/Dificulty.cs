using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificulty : MonoBehaviour
{
    [SerializeField] float puntos;
    private float probabilidad;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
