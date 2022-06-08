using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChange : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void close()
    {
        Application.Quit();
    }

    public void historia()
    {
        if(PlayerPrefs.GetInt("primerJuego") == 0)
        {
            SceneManager.LoadScene(2);
            PlayerPrefs.SetInt("primerJuego", 1);
            PlayerPrefs.Save();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}