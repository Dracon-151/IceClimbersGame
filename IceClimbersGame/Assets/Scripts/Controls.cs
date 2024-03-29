﻿//Script que controla los inputs de movimiento del player
//Creado por Alexis Alvarado.
//Fecha: 02/05/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls: MonoBehaviour
{
    [SerializeField] LayerMask controls;

    //Este vector es el que se debe usar con el player
    public Vector2 direction;

    private Vector2 initial;

    private BoxCollider2D[] hitboxs;

    private int dir;
    private int up;

    void Start()
    {
        //Inicialización de variables
        initial = Vector2.zero;
        hitboxs = this.GetComponentsInChildren<BoxCollider2D>();
        activate();


        //Se posicionan los botones de acuerdo a las dimensiones de la pantalla
        hitboxs[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2 / 20, Screen.height * 3f / 20, 22));
        hitboxs[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 4.5f / 20, Screen.height * 3f / 20, 22));
        hitboxs[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 18f / 20, Screen.height * 3f / 20, 22));
    }

    void Update()
    {
        //Llama a la función de control
        normal();
    }

    //Registra los inputs y los convierte en enteros que indican la direccion
    void normal()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch i in Input.touches)
            {
                initial = Camera.main.ScreenToWorldPoint(i.position);

                if (initial.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x)
                {
                    if (Input.touchCount < 2)
                    {
                        up = 0;
                    }
                    if (Physics2D.OverlapPoint(initial, controls) == hitboxs[1])
                    {
                        dir = 1;
                    }
                    else if (Physics2D.OverlapPoint(initial, controls) == hitboxs[0])
                    {
                        dir = -1;
                    }
                }

                if (initial.x >= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x)
                {
                    if (Input.touchCount < 2)
                    {
                        dir = 0;
                    }
                    if (Physics2D.OverlapPoint(initial, controls) == hitboxs[2])
                    {
                        if (i.phase == TouchPhase.Began) up = 1;
                        else up = 0;
                    }
                }

                direction = new Vector2(dir, up);
            }
        }
        else
        {
            dir = 0;
            up = 0;
            direction = Vector2.zero;
        }
    }

    //Activa los gameobjects de los botones en caso de ser necesario
    void activate()
    {
        hitboxs[0].gameObject.SetActive(true);
        hitboxs[1].gameObject.SetActive(true);
        hitboxs[2].gameObject.SetActive(true);
    }
}
