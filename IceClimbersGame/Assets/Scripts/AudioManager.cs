//Script que administra la musica a traves de las escenas
//Creado por Alexis Alvarado.
//Fecha: 05/05/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject[] musicObject;

    public int id;
    private float initialVolume;

    private AudioSource soundSource;

    private void Start()
    {
        //Incializa las variables de volumen y define las instancias de audiomanager, eliminando objetos duplicados
        //para continuar la musica despues de un cambio de escena
        if(PlayerPrefs.GetFloat("Musicvolume") == 0)
        {
            PlayerPrefs.SetFloat("Musicvolume", 1);
            PlayerPrefs.Save();
        }

        DontDestroyOnLoad(this.gameObject);

        if (GameObject.FindObjectOfType<Player>()) id = 0;
        else id = 1;

        soundSource = this.GetComponent<AudioSource>();

        musicObject = GameObject.FindGameObjectsWithTag("AudioManager");
        if (musicObject.Length > 1)
        {
            for (int i = 1; i < musicObject.Length; i++)
            {
                if (musicObject[0].GetComponent<AudioManager>().id ==
                    musicObject[i].GetComponent<AudioManager>().id)
                {
                    Destroy(musicObject[i]);
                }
                else
                {
                    Destroy(musicObject[0]);
                }
            }
        }

        initialVolume = soundSource.volume;
    }

    private void Update()
    {
        //Ajusta el volumen en tiempo real y pausa la música en caso de que se active el menú de pausa
        if (Time.timeScale == 0) soundSource.Pause();
        else if (!soundSource.isPlaying && Time.timeScale != 0) soundSource.Play();

        soundSource.volume = initialVolume * (0.2f * PlayerPrefs.GetFloat("Musicvolume"));
    }
}

