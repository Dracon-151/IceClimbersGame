//Script para controlar el player
//Creado por Eduardo Gonzalez
//Fecha: 02/05/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //Declaración de variables serializadas para aparecer en el editor.
    [SerializeField] private float movingSpeed;

    //Declaración de variables privadas.
    private Camera mainCamera;
    private Vector2 screenBounds;

    //Declaración de variables publicas.
    public float choke;
    public GameObject[] levels;

    //Funcion que se ejecuta en cuanto se llama al script.
    private void Start()
    {
        //Asignación de variables.
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        //Se llama a la función de fondo infinito.
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }

    //Función para el fondo infinito
    void loadChildObjects(GameObject obj)
    {
        //Se define el tamaño de la imagen de fondo y cuantas de estas se requieren para abarcar toda la pantalla.
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke ;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;

        //Se define la cantidad de imagenes que estaran en el fondo.
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    //Funcion para repintar el fondo junto con el movimientod de la camara.
    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;

            //Reposición de los objetos del fondo para que aparezcan delante de la camara.
            if(transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjectHeight - 0.5f)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
            }else if(transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjectHeight)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfObjectHeight * 2, firstChild.transform.position.z);
            }
        }
    }

    //Función que inicia el fondo infinito. 
    private void LateUpdate()
    {
        foreach(GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }

    //Se asigna el movimiento de la camara.
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * movingSpeed;
    }
}
