//Script que controla las llamadas a las api de Google Play
//Creado por Alexis Alvarado.
//Fecha: 08/06/2022
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSrvcs: MonoBehaviour
{
    public static PlayGamesPlatform platform;

    public bool success;

    private void Start()
    {
        //Inicia sesión si no se ha rechazado el inicio de sesión antes
        if (PlayerPrefs.GetFloat("Sesion") == 0)
        {
            login();
        }
    }


    //Inicia sesión en Google Play
    public void login()
    {
        if (platform == null)
        {
            PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success) Debug.Log("Logged in successfully");
            else
            {
                Debug.Log("Not logged in");
                PlayerPrefs.SetFloat("Sesion", 1);
                PlayerPrefs.Save();
            }
        });
    }

    //Devuelve true si el usuario ha iniciado sesión, sino devuelve false
    public bool user()
    {
        return Social.localUser.authenticated;
    }

}
