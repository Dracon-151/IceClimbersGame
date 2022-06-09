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

    private bool activePause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            Time.timeScale = 1;
        }
        
    }

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
