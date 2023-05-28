//Script que controla el menú de pausa y de mmuerte
//Creado por Alexis Alvarado.
//Fecha: 01/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlays : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject[] canvasImages;
    [SerializeField] private Text[] scoreTexts;

    public bool activePause = false;


    //Si el juego está en ejecución, detiene la ejecución y activa el menú de pausa, desactiva el ui de juego
    //Caso contrario continua la ejecución, desactiva el menú de pausa y activa el ui de juego
    public void pause()
    {
        activePause = !activePause;
        pauseMenu.SetActive(activePause);

        foreach(GameObject i in canvasImages)
        {
            i.SetActive(!activePause);
        }
        
        if (activePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            float timeScale = 1;

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().powerup == "SlowMotion") timeScale = 0.5f;

            Time.timeScale = timeScale;
        }
        
    }

    //Detiene la ejecución y activa el menú de muerte, desactiva el ui de juego
    public void death(int altura, int score)
    {
        activePause = true;
        deathMenu.SetActive(activePause);

        foreach (GameObject i in canvasImages)
        {
            i.SetActive(!activePause);
        }

        scoreTexts[0].text = altura.ToString();
        scoreTexts[1].text = score.ToString();

        if (activePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
