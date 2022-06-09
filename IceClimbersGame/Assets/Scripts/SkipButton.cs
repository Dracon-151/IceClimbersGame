using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("primerJuego") == 0 && PlayerPrefs.GetInt("ultimaEscena") == 1)
        {
            PlayerPrefs.SetInt("primerJuego", 1);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.GetInt("ultimaEscena") == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
