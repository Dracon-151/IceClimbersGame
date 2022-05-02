using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles: MonoBehaviour
{
    //Script que controla los inputs de movimiento del player

    [SerializeField] LayerMask controles;

    //Este vector es el que se debe usar con el player
    public Vector2 direccion;

    private Vector2 inicial;

    private BoxCollider2D[] hitboxs;

    private int dir;
    private int up;

    void Start()
    {
        //Inicialización de variables
        inicial = Vector2.zero;
        hitboxs = this.GetComponentsInChildren<BoxCollider2D>();
        activar();


        //Se posicionan los botones de acuerdo a las dimensiones de la pantalla
        hitboxs[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2 / 20, Screen.height * 2.5f / 20, 22));
        hitboxs[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 4.5f / 20, Screen.height * 2.5f / 20, 22));
        hitboxs[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 18f / 20, Screen.height * 2.5f / 20, 22));
    }

    void Update()
    {
        normal();
    }

    //Registra los inputs y los convierte en enteros que indican la direccion
    void normal()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch i in Input.touches)
            {
                inicial = Camera.main.ScreenToWorldPoint(i.position);

                if (inicial.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x)
                {
                    if (Input.touchCount < 2)
                    {
                        up = 0;
                    }
                    if (Physics2D.OverlapPoint(inicial, controles) == hitboxs[1])
                    {
                        dir = 1;
                    }
                    else if (Physics2D.OverlapPoint(inicial, controles) == hitboxs[0])
                    {
                        dir = -1;
                    }
                }

                if (inicial.x >= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x)
                {
                    if (Input.touchCount < 2)
                    {
                        dir = 0;
                    }
                    if (Physics2D.OverlapPoint(inicial, controles) == hitboxs[2])
                    {
                        up = 1;
                    }
                }

                direccion = new Vector2(dir, up);
            }
        }
        else
        {
            dir = 0;
            up = 0;
            direccion = Vector2.zero;
        }
    }

    //Activa los gameobjects de los botones en caso de ser necesario
    void activar()
    {
        hitboxs[0].gameObject.SetActive(true);
        hitboxs[1].gameObject.SetActive(true);
        hitboxs[2].gameObject.SetActive(true);
    }
}
