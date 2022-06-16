//Script para controlar la generación de niveles
//Creado por Eduardo Gonzalez
//Fecha: 02/05/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Declaración de variables serializadas para aparecer en el editor.
    [SerializeField] private Transform level_part_0;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;
    [SerializeField] private Transform spawner;
    [SerializeField] public Transform despawnerinador;
    [SerializeField] private float spawnTime;

    //Declaración de variables privadas.
    private Vector3 lastEndPosition;

    //Funcion que se habilita nada mas empezar el programa.
    private void Awake()
    {
        //Busca la posicion final del primer nivel.
        lastEndPosition = level_part_0.Find("EndPosition").position;
        
        //Crea x cantidad de niveles iniciales.
        int startingSpawnLevelParts = 0;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }

        //Inicia la generación de niveles mediante recursividad.
        StartCoroutine(SpawnTimer());
    }

    private void Update()
    {

    }

    //Función asignar un tiempo de aparicion a los niveles.
    private IEnumerator SpawnTimer()
    {
        //Se llama al generador de niveles.
        SpawnLevelPart();
        //Se le asigna el tiempo de espera entre niveles.
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnTimer());
    }

    //Función para la aleatoriedad de los niveles.
    private void SpawnLevelPart()
    {
        //Se elige un nivel aleatorio de la lista de niveles.
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        //Se llama al generador de niveles.
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart);
        //Se reasigna la nueva posición final del ultimo nivel generado.
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        
    }

    //Funcion que genera los niveles en el juego.
    private Transform SpawnLevelPart(Transform levelPart)
    {
        //Crea el nivel dentro del juego.
        Transform levelPartTransform = Instantiate(levelPart, spawner.position, Quaternion.identity);
        return levelPartTransform;
    }
}
