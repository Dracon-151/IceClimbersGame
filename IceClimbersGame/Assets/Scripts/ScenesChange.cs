//Script que controla las trancisiones entre pantallas y botones del men�
//Creado por Alexis Alvarado y Eduardo Gonzalez.
//Fecha: 02/06/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChange : MonoBehaviour
{
    public void Start()
    {
        //Ajusta los fps de la aplicaci�n a 60
        Application.targetFrameRate = 60;
        if(SceneManager.GetActiveScene().buildIndex == 0) PlayerPrefs.SetInt("ultimaEscena", 0);
        PlayerPrefs.Save();
    }
    
    //Carga la escena solicitada y desactiva la pausa
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    //Cierra la aplicaci�n
    public void close()
    {
        Application.Quit();
    }

    //Detecta si es la primera vez que se abre la aplicaci�n para mostrar la historia
    public void historia()
    {
        if(PlayerPrefs.GetInt("primerJuego") == 0)
        {
            PlayerPrefs.SetInt("ultimaEscena", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    //Muestra las tablas de clasificaci�n
    public void lb()
    {
        Social.ShowLeaderboardUI();
    }

    //Inicia sesi�n en Google Play
    public void login()
    {
        GPGSrvcs log = GameObject.FindObjectOfType<GPGSrvcs>();

        PlayerPrefs.SetFloat("Sesion", 0);
        PlayerPrefs.Save();
        log.login();

    }
}