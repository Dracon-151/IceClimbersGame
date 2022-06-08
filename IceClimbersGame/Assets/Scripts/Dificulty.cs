using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificulty : MonoBehaviour
{
    [SerializeField] float puntos;
    private float probabilidad;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score <= puntos)
        {
            Destroy(this.gameObject);
        }
        else
        {
            probabilidad = Random.Range(0, 100);
            if (probabilidad > (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score / 100))
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
