using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChange : MonoBehaviour
{
    public void Start()
    {
        //PlayerPrefs.SetInt("primerJuego", 0);
        if(SceneManager.GetActiveScene().buildIndex == 0) PlayerPrefs.SetInt("ultimaEscena", 0);
        PlayerPrefs.Save();
    }
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void close()
    {
        Application.Quit();
    }

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

    public void lb()
    {
        Social.ShowLeaderboardUI();
    }

    public void achieve()
    {
        if(GameObject.FindObjectOfType<GPGSrvcs>().user())Social.ShowAchievementsUI();
    }

    public void login()
    {
        GPGSrvcs log = GameObject.FindObjectOfType<GPGSrvcs>();

        if (!log.user())
        {
            log.login();
            PlayerPrefs.SetFloat("Sesion", 0);
            PlayerPrefs.Save();
        }
    }
}