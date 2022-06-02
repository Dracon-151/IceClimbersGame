using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChange : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Historia()
    {
        SceneManager.LoadScene("Historia");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
